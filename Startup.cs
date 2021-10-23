using ComercioDigitalDemoAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ComercioDigitalDemoAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "API para gestão de comércio eletrônico",
                    Version = "v1"
                });

                var identitySettingsSection = Configuration.GetSection("ConnectionStrings");
                services.Configure<ConnectionStringsModel>(identitySettingsSection);
            });
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        { 
            app.UseSwagger();

            app.UseSwaggerUI(c => 
            { 
                c.SwaggerEndpoint("/swagger/v1/swagger.json", 
                "API para gestão de comércio eletrônico");

                c.RoutePrefix = string.Empty;

            });

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