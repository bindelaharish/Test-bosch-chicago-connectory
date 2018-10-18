#pragma warning disable 1591
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RB.JobAssistant.Util;
using Serilog;

namespace RB.JobAssistant.Data.Manage
{
    public class DbServicesHelper
    {
        private readonly AppSettingsConfig _config;

        public DbServicesHelper(AppSettingsConfig configWrapper)
        {
            _config = configWrapper;
        }

        public string GetDbConnectionString()
        {
            var myConnStringFromConfig = _config.GetConfigValue("RB_JOB_ASSISTANT_DB");
            return myConnStringFromConfig;
        }

        public IServiceCollection AddDbContextAndBindDbOptions(IServiceCollection services)
        {
            var myConnStringFromConfig = GetDbConnectionString();
            Log.Logger.Information("Logged DB connection string read from Configuration object: " +
                                   myConnStringFromConfig);
            services.AddDbContext<JobAssistantContext>(p => p.UseMySql(myConnStringFromConfig));
            return services;
        }
    }
}