using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebApiAutores.Controllers;
using WebApiAutores.Servicios;

namespace WebApiAutores
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices (IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));

            //AddTransient sirve para crear una nueva instancia de una clase o interfaz para cada llamada Http realizada
            //BENEFICIO: Es bueno para simples funciones que cumplen cierta funcionalidad única sin necesitar almacenar data temporal.

            services.AddTransient<IServicio, ServicioA>(); 
            //services.AddTransient<ServicioB>(); 

            //AddScoped sirve para crear una nueva instancia la cual prevalecerá existente para todas las llamadas http realizadas (para un solo cliente)
            //BENEFICIO: Sirve para cuando se necesita la misma data para cada vez que se realiza una petición (como en el AddDbContext que extrae y mantiene la información de la conexión a la BD)

            //services.AddScoped<IServicio, ServicioB>(); 

            //AddSingleton sirve para crear una instancia la cual será la misma para todas las llamadas http realizadas para todos los clientes que la realicen.
            //BENEFICIO: Sirve para cuando se tiene una capa de caché (en memoria) para servirla de forma rápida a los usuarios. Todos los usuarios van a tener la misma data compartida entre todos.

            //services.AddSingleton<IServicio, ServicioB>();

            services.AddTransient<ServicioTransient>(); 
            services.AddScoped<ServicioScoped>();
            services.AddSingleton<ServicioSingleton>();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
