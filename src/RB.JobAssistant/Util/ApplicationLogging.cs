#pragma warning disable 1591
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;

namespace RB.JobAssistant.Util
{
    public static class ApplicationLogging
    {
        private static bool _isInitialized;

        static ApplicationLogging()
        {
            InitializeWithBasicConfiguration();
        }

        public static ILoggerFactory LoggerFactory { get; } = new LoggerFactory().AddSerilog(dispose: true);

        public static void InitializeWithBasicConfiguration()
        {
            Log.Logger = CreateSerilogLogger(null);
            _isInitialized = true;
        }

        public static void InitializeWithDefaultAppSettings()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            var configuration = builder.Build();
            Log.Logger = CreateSerilogLogger(configuration);
            _isInitialized = true;
        }

        public static void InitializeWithConfiguration(IConfigurationRoot configuration)
        {
            Log.Logger = CreateSerilogLogger(configuration);
            _isInitialized = true;
        }

        public static ILogger<T> CreateTypeLogger<T>()
        {
            if (!_isInitialized)
                InitializeWithBasicConfiguration();

            return LoggerFactory.CreateLogger<T>();
        }

        private static Logger CreateSerilogLogger(IConfigurationRoot configuration)
        {
            if (configuration != null)
            {
                return new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
            }
            var logPath = Path.Combine("./", "log-{Date}.txt");
            return new LoggerConfiguration().MinimumLevel.Debug().WriteTo.RollingFile(logPath).CreateLogger();
        }
    }
}