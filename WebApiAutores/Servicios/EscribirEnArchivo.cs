namespace WebApiAutores.Servicios
{
    public class EscribirEnArchivo : IHostedService
    {
        private readonly IWebHostEnvironment env;
        private readonly string nombreArchivo = "Archivo 1.txt";
        private Timer timer;

        public EscribirEnArchivo(IWebHostEnvironment env)
        {
            this.env = env;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            //Se ejecuta una vez cuando se ejecuta el WebApi
            Escribir("Proceso Iniciado");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer.Dispose();
            //Se ejecuta una vez cuando se detiene la ejecución del WebApi
            //Este método no necesariamente se va a ejecutar (Por ejemplo, si se detiene por un error catastrófico)
            Escribir("Proceso Finalizado");
            return Task.CompletedTask;
        }
        public void DoWork(object state)
        {
            Escribir($"Proceso en ejecución: {DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")}");
        }
        public void Escribir(string mensaje)
        {
            var ruta = $@"{env.ContentRootPath}\wwwroot\{nombreArchivo}";
            using(StreamWriter writer = new StreamWriter(ruta, append: true))
            {
                writer.WriteLine(mensaje);
            }
        }
    }
}
