namespace WebApiAutores.Servicios
{
    public interface IServicio
    {
        Guid ObtenerScoped();
        Guid ObtenerSingleton();
        Guid ObtenerTransient();
        void RealizarTarea();
    }
    public class ServicioA : IServicio
    {
        private readonly ILogger<ServicioA> logger;
        private readonly ServicioTransient sTransient;
        private readonly ServicioScoped sScoped;
        private readonly ServicioSingleton sSingleton;

        public ServicioA(ILogger<ServicioA> logger, ServicioTransient sTransient, ServicioScoped sScoped, ServicioSingleton sSingleton)
        {
            this.logger = logger;
            this.sTransient = sTransient;
            this.sScoped = sScoped;
            this.sSingleton = sSingleton;
        }

        public Guid ObtenerTransient() { return sTransient.Guid; }
        public Guid ObtenerScoped() { return sScoped.Guid; }
        public Guid ObtenerSingleton() { return sSingleton.Guid; }


        public void RealizarTarea()
        {

        }
    }
    public class ServicioB : IServicio
    {
        public Guid ObtenerScoped()
        {
            throw new NotImplementedException();
        }

        public Guid ObtenerSingleton()
        {
            throw new NotImplementedException();
        }

        public Guid ObtenerTransient()
        {
            throw new NotImplementedException();
        }

        public void RealizarTarea()
        {
            throw new NotImplementedException();
        }
    }
    public class ServicioTransient
    {
        public Guid Guid = Guid.NewGuid();
    }
    public class ServicioScoped
    {
        public Guid Guid = Guid.NewGuid();
    }
    public class ServicioSingleton
    {
        public Guid Guid = Guid.NewGuid();
    }
}
