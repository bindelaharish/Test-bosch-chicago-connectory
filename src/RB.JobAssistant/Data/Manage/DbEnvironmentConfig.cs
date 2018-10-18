#pragma warning disable 1591
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace RB.JobAssistant.Data.Manage
{
    public class DbEnvironmentConfig
    {
        private readonly IConfigurationRoot _config;
        public DbEnvironmentConfig()
        {
            var builder = new ConfigurationBuilder();
            builder.AddEnvironmentVariables(); // TODO: Investigate using environmental variable prefixes (such as MYSQL_ )
            _config = builder.Build();
        }

        public DbEnvironmentConfig(Dictionary<string, string> configDictionary)
        {
            var builder = new ConfigurationBuilder();
            builder.AddInMemoryCollection(configDictionary);
            _config = builder.Build();
        }

        public string GenerateMySqlConnectionString()
        {
            return $@"server={GetConfigValue("MYSQL_SERVER")};database={GetConfigValue("MYSQL_DATABASE")};uid={
                    GetConfigValue("MYSQL_USER_ID")
                };pwd={GetConfigValue("MYSQL_USER_PASSWORD")};";
        }

        private string GetConfigValue(string configKey)
        {
            var configValue = _config[configKey];
            if (string.IsNullOrEmpty(configValue))
                return "(null)";
            return configValue;
        }
    }
}
