namespace WebApiAutores.Middlewares
{
    public static class LoggerResponseHTTPMdExtensions
    {
        public static IApplicationBuilder UseLoggerResponseHTTP(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LoggerResponseHTTPMd>();
        }
    }

    public class LoggerResponseHTTPMd
    {
        private readonly RequestDelegate siguiente;
        private readonly ILogger<LoggerResponseHTTPMd> logger;

        public LoggerResponseHTTPMd(RequestDelegate siguiente, ILogger<LoggerResponseHTTPMd> logger)
        {
            this.siguiente = siguiente;
            this.logger = logger;
        }

        //Para ser invocado como un middleware necesita un Invoke o InvokeAsync
        public async Task InvokeAsync(HttpContext contexto)
        {
            using (var ms = new MemoryStream())
            {
                var cuerpoOriginalRespuesta = contexto.Response.Body;
                contexto.Response.Body = ms;

                await siguiente(contexto);

                ms.Seek(0, SeekOrigin.Begin);
                string respuesta = new StreamReader(ms).ReadToEnd();
                ms.Seek(0, SeekOrigin.Begin);

                await ms.CopyToAsync(cuerpoOriginalRespuesta);
                contexto.Response.Body = cuerpoOriginalRespuesta;

                logger.LogInformation(respuesta);
            }
        }
    }
}
