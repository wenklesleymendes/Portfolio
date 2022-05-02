using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ServidorDePontos.Aplication.Interface;
using ServidorDePontos.Aplication.Service;
using ServidorDePontos.Repository;
using ServidorDePontos.Repository.Interfaces;
using ServidorDePontos.Repository.Repository;

namespace ServidorDePontos.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var connection = Configuration["ConnectionStrings:ServidorDePontosDB"];
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlite(connection)
            );

            services.AddControllers();
            services.AddMemoryCache();
            services.AddScoped<IGameResultService, GameResultService>();
            services.AddScoped<IGameResultRepository, GameResultRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ServidorDePontos.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ServidorDePontos.API v1"));
            }

            app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());

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
