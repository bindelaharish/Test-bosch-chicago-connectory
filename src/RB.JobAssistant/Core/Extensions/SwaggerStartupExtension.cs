using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace RB.JobAssistant.Core
{
    public static class SwaggerConfig
    {
        private const string SwaggerDocumentName = "v1";
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(SwaggerDocumentName, new Info
                {
                    Title = "rb.pt.na.ecommerce.order",
                    Version = "v1",
                    Description = "Microservice dedicated to orders."

                });
                c.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RB.PT.NA.Ecommerce.Orders.xml"));
                c.IgnoreObsoleteActions();
                

            });
            return services;
        }

        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app)
        {
            //swagger config here-> generate json
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "api-docs/{documentName}/swagger.json";

            });

            //set up swagger UI
            app.UseSwaggerUI(c =>
            {
                // c.RoutePrefix = "help";//if you want a diffrent route name for swagger documentation
                c.SwaggerEndpoint($"/api-docs/{SwaggerDocumentName}/swagger.json", "rb.pt.na.ecommerce.orders");

            });
            return app;
        }
    }
}
