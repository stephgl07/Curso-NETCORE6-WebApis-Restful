using System.ComponentModel.DataAnnotations;

namespace WebApiAutores.Entidades
{
    public class beAutor: IValidatableObject
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        //public int Menor { get; set; }
        //public int Mayor { get; set; }
        public List<beLibro> Libros { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!String.IsNullOrEmpty(Nombre))
            {
                var primeraLetra = Nombre[0].ToString();
                if(primeraLetra != primeraLetra.ToUpper())
                {
                    yield return new ValidationResult("La primera letra debe ser mayúscula",
                            new string[] { nameof(Nombre) });
                }
            }
            /*
            if(Menor > Mayor)
            {
                yield return new ValidationResult("Este valor no puede ser más grande que el campo mayor",
                        new string[] { nameof (Menor) });
            }*/
        }
    }
}
