#pragma warning disable 1591
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using RB.JobAssistant.Util;
using RB.JobAssistant.Data.Manage;

namespace RB.JobAssistant
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddInMemoryCollection(CommandLineHelper.CliArguments)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddCommandLine(args, CommandLineHelper.GetDefaultSwitchMappings())
                .AddEnvironmentVariables()
                .Build();

            if (!DbSchemaArguments.ProcessArguments(config))
            {
                var host = new WebHostBuilder()
                    .UseConfiguration(config)
                    .UseKestrel()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseIISIntegration()
                    .UseStartup<Startup>()
                    // Instead of .UseUrls("http://*:8080"), let's set it from the command line using --server.urls "http://*.8080/" */
                    .Build();

                host.Run();
            }
        }
    }
}
