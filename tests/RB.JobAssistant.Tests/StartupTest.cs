using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RB.JobAssistant.Data;
using RB.JobAssistant.MultiTenant;
using RB.JobAssistant.Repo;
using RB.JobAssistant.Util;
using Serilog;

namespace RB.JobAssistant.Tests
{
    public class StartupTest
    {
        public StartupTest(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder();
            Configuration = builder.Build();

            ApplicationLogging.InitializeWithBasicConfiguration();

            Log.Information("Finishing up in ASP.NET Core Startup method!");
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSingleton(logger => Log.Logger);
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<DbContext, JobAssistantContext>();

            var uniqueInMemoryDbId = RandomNumberHelper.NextInteger();
            services.AddDbContext<JobAssistantContext>(options => options.UseInMemoryDatabase("test_in-memory_DB-" + uniqueInMemoryDbId));

            services.AddMultitenancy<Tenant, CachingTenantResolver>();

            Log.Information("Finishing up in configuration of test-oriented framework services!");
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMultitenancy<Tenant>();

            app.UseMvcWithDefaultRoute();

            var dbContext = app.ApplicationServices.GetService<JobAssistantContext>();
            dbContext.LoadSampleData();
        }
    }
}
