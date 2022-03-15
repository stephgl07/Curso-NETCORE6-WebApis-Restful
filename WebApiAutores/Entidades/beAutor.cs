using System.ComponentModel.DataAnnotations;

namespace WebApiAutores.Entidades
{
    public class beAutor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<beLibro> Libros { get; set; }

    }
}
