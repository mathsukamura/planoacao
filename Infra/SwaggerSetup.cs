using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.IO;

namespace apiplanoacao.Infra
{
    public static class SwaggerSetup
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            return services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Template .Net Core",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Matheus Yuri De Melo Barros",
                        Email = "matheus.yuri.melo@gmail.com",
                    }
                });
                var xmlPath = Path.Combine("api-doc.xml");
                opt.IncludeXmlComments(xmlPath);
            });
        }
        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            return app.UseSwagger().UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Plano de Ação");
            });
        }
    }
}
