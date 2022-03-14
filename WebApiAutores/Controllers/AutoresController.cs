using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entidades;
using WebApiAutores.Filtros;
using WebApiAutores.Servicios;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IServicio servicio;
        private readonly ServicioTransient sTransient;
        private readonly ServicioScoped sScoped;
        private readonly ServicioSingleton sSingleton;
        private readonly ILogger<AutoresController> logger;

        public AutoresController(ApplicationDbContext context, IServicio servicio,
            ServicioTransient sTransient, ServicioScoped sScoped, ServicioSingleton sSingleton, ILogger<AutoresController> logger)
        {
            this.context = context;
            this.servicio = servicio;
            this.sTransient = sTransient;
            this.sScoped = sScoped;
            this.sSingleton = sSingleton;
            this.logger = logger;
        }

        [HttpGet("GUID")]
        //[ResponseCache(Duration = 10)]
        [ServiceFilter(typeof(MiFiltroDeAccion))]
        public ActionResult ObtenerGuid()
        {
            return Ok(new { 
                AutoresController_Transient = sTransient.Guid,
                ServicioA_Transient = servicio.ObtenerTransient(),
                AutoresController_Scoped = sScoped.Guid,
                ServicioA_Scoped = servicio.ObtenerScoped(),
                AutoresController_Singleton = sSingleton.Guid,
                ServicioA_Singleton = servicio.ObtenerSingleton(),
            
            });
        }


        [HttpGet] // api/autores
        [HttpGet("listado")] // api/autores/listado
        [HttpGet("/listado")] // listado
        //[Authorize]
        public async Task<ActionResult<List<beAutor>>> Get()
        {
            throw new NotImplementedException();
            logger.LogInformation("Estamos obteniendo los autores");
            servicio.RealizarTarea();
            var lista = await context.Autores.Include(x => x.Libros).ToListAsync();
            return lista;
        }

        [HttpGet("primero")] // api/autores/primero
        public async Task<IActionResult> PrimerAutor()
        {
            var autor = await context.Autores.FirstOrDefaultAsync();
            return Ok(autor);
        }

        [HttpGet("PrimerAutor/{id}")] // api/autores/{id}
        public async Task<ActionResult<beAutor>> PrimerAutor0(int id)
        {
            var autor = await context.Autores.FirstOrDefaultAsync(x => x.Id == id);
            if(autor is null)
            {
                return NotFound();
            }
            return Ok(autor);
        }

        [HttpGet("{id:int}")] // api/autores/{id} (con validacion de tipo)
        public async Task<ActionResult<beAutor>> PrimerAutor(int id)
        {
            var autor = await context.Autores.FirstOrDefaultAsync(x => x.Id == id);
            if (autor is null)
            {
                return NotFound();
            }
            return Ok(autor);
        }

        [HttpGet("{id:int}/{param2}")] // api/autores/{id}/{param2} con doble parametro
        public async Task<ActionResult<beAutor>> PrimerAutor(int id, string param2)
        {
            var autor = await context.Autores.FirstOrDefaultAsync(x => x.Id == id);
            if (autor is null)
            {
                return NotFound();
            }
            return Ok(autor);
        }

        [HttpGet("PrimeroAutor3Params")] // api/autores/PrimeroAutor3Params?id={id}&param2={param2}&param3={param3}
        public async Task<ActionResult<beAutor>> PrimerAutor([FromQuery] int id, [FromQuery] string param2, [FromQuery] string param3)
        {
            var autor = await context.Autores.FirstOrDefaultAsync(x => x.Id == id);
            if (autor is null)
            {
                return NotFound();
            }
            return Ok(autor);
        }

        [HttpGet("{nombre}")] // api/autores/{nombre}
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
        public async Task<ActionResult> Post([FromBody] beAutor autor)
        {
            var existeAutorConMismoNombre = await context.Autores.AnyAsync(x => x.Nombre == autor.Nombre);
            if (existeAutorConMismoNombre)
            {
                return BadRequest($"Ya existe un autor con el nombre {autor.Nombre}");
            }
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
