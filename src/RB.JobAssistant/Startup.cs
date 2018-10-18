#pragma warning disable 1591
using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RB.JobAssistant.Util;
using RB.JobAssistant.Data;
using RB.JobAssistant.Data.Manage;
using RB.JobAssistant.MultiTenant;
using RB.JobAssistant.Repo;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RB.JobAssistant
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            ApplicationLogging.InitializeWithBasicConfiguration();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.AddSingleton(logger => Log.Logger);

            services.AddScoped<IRepository, Repository>();
            services.AddScoped<DbContext, JobAssistantContext>();

            services.AddMultitenancy<Tenant, CachingTenantResolver>();

            var serviceHelper = new DbServicesHelper(new AppSettingsConfig());
            serviceHelper.AddDbContextAndBindDbOptions(services);

            // Add Swagger API information.
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Title = "Bosch Job Assistant API",
                    Version = "v1",
                    Description = "Job Assistant Web API powered by Microsoft ASP.NET Core",
                    TermsOfService = "Limited",
                    Contact = new Contact
                    {
                        Name = "Kevin Wittmer",
                        Email = "kevin.wittmer@bosch.com",
                        Url = "www.bosch.com"
                    }
                });
                AddXmlCommentsPath(options);
                options.DocumentFilter<OptionalPathParamDocumentFilter>();
            });
        }

        public void AddXmlCommentsPath(SwaggerGenOptions options)
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var absoluteXmlPath = Path.Combine(basePath, "RB.JobAssistant.xml");
            if (File.Exists(absoluteXmlPath))
                options.IncludeXmlComments(absoluteXmlPath);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/error");

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swagger, httpReq) => { 
                    swagger.Host = httpReq.Host.Value; 
                });
            });

            /* 
             * Swagger XML is at:  http://somehost:8080/swagger/v1/swagger.json
             */
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs"); });

            app.UseMultitenancy<Tenant>();

            app.UseMvcWithDefaultRoute();
        }
    }
}
