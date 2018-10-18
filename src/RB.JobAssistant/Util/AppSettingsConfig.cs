#pragma warning disable 1591
using System.IO;
using Microsoft.Extensions.Configuration;

namespace RB.JobAssistant.Util
{
    public class AppSettingsConfig
    {
        private readonly IConfigurationRoot _config;

        public AppSettingsConfig() : this (Directory.GetCurrentDirectory())
        {
        }

        public AppSettingsConfig(string basePath)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(basePath);
            builder.AddJsonFile("appsettings.json", true, true);
            builder.AddEnvironmentVariables();
            _config = builder.Build();
        }

        public string GetConfigValue(string configKey)
        {
            var configValue = _config[configKey];
            if (string.IsNullOrEmpty(configValue))
                return "(not set)";
            return configValue;
        }
    }
}