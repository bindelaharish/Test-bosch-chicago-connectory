using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RB.JobAssistant.Data;
using RB.JobAssistant.Data.Manage;

namespace RB.JobAssistant.Tests.Data
{
    public class TestContextHelper
    {
        public DbContextOptions<JobAssistantContext> Options { get; }

        public TestContextHelper(string environmentClass)
        {
            var optionsBuilder = new DbContextOptionsBuilder<JobAssistantContext>();
            if (!string.IsNullOrEmpty(environmentClass) && environmentClass.Contains("test")) {
                optionsBuilder.UseInMemoryDatabase(environmentClass);
            }
            else {
                var envConfig = new DbEnvironmentConfig(new Dictionary<string, string>
                    {
                        {"MYSQL_SERVER", "localhost"},
                        {"MYSQL_DATABASE", "JobAssistant"},
                        {"MYSQL_USER_ID", "root"},
                        {"MYSQL_USER_PASSWORD", "MiSs-5.7.17$"}
                    });
                string mySqlConnectionString = envConfig.GenerateMySqlConnectionString();
                optionsBuilder.UseMySql(mySqlConnectionString);
            }

            this.Options = optionsBuilder.Options;
        }
    }
}
