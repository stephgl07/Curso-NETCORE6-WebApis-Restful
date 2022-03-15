using System.ComponentModel.DataAnnotations;

namespace WebApiAutores.Entidades
{
    public class beAutor
    {
        public int Id { get; set; }
        [StringLength(maximumLength:120)]
        public string Nombre { get; set; }

    }
}
