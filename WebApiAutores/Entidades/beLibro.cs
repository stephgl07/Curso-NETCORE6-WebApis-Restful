namespace WebApiAutores.Entidades
{
    public class beLibro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int AutorId { get; set; }
        public beAutor Autor { get; set; }

    }
}
