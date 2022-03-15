using System.ComponentModel.DataAnnotations;

namespace WebApiAutores.Entidades
{
    public class beLibro
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 120)]
        public string Titulo { get; set; }

    }
}
