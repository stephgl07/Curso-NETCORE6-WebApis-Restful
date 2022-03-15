using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.DTOs;
using WebApiAutores.Entidades;
using WebApiAutores.Filtros;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public AutoresController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<beAutor>>> Get()
        {
            var lista = await context.Autores.ToListAsync();
            return lista;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<beAutor>> PrimerAutor(int id)
        {
            var autor = await context.Autores.FirstOrDefaultAsync(x => x.Id == id);
            if (autor is null)
            {
                return NotFound();
            }
            return Ok(autor);
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<beAutor>> PrimerAutor(string nombre)
        {
            var autor = await context.Autores.FirstOrDefaultAsync(x => x.Nombre.Contains(nombre));
            if (autor is null)
            {
                return NotFound();
            }
            return Ok(autor);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AutorCreacionDTO autorDTO)
        {
            var existeAutorConMismoNombre = await context.Autores.AnyAsync(x => x.Nombre == autorDTO.Nombre);
            if (existeAutorConMismoNombre)
            {
                return BadRequest($"Ya existe un autor con el nombre {autorDTO.Nombre}");
            }

            var autor = mapper.Map<beAutor>(autorDTO);

            context.Add(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put([FromBody] beAutor autor, int id)
        {
            if(autor.Id != id)
            {
                return BadRequest("El id del autor no coincide con el id de la URL");
            }
            var existe = await context.Autores.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }
            context.Update(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Autores.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound("No existe registro con el id ingresado");
            }
            context.Remove(new beAutor()
            {
                Id = id
            });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
