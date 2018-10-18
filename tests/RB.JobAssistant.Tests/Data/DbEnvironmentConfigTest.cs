using RB.JobAssistant.Data.Manage;
using System.Collections.Generic;
using Xunit;

namespace RB.JobAssistant.Tests.Data
{
    public class DbEnvironmentConfigTest
    {
        [Fact]
        public void DbEnvironmentReadTest()
        {
            var dbCredentials = new Dictionary<string, string>
            {
                {"MYSQL_SERVER", "localhost"},
                {"MYSQL_DATABASE", "JobAssistant"},
                {"MYSQL_USER_ID", "root"},
                {"MYSQL_USER_PASSWORD", "MiSs-5.7.17$"}
            };

            var configUnderTest = new DbEnvironmentConfig(dbCredentials);
            Assert.NotNull(configUnderTest);

            var connectionString = configUnderTest.GenerateMySqlConnectionString();
            Assert.Equal(@"server=localhost;database=JobAssistant;uid=root;pwd=MiSs-5.7.17$;", connectionString);
        }

        [Fact]
        public void DbEnvironmentReadTestWithEmptyInputTest()
        {
            var dbCredentials = new Dictionary<string, string>();

            var configUnderTest = new DbEnvironmentConfig(dbCredentials);
            Assert.NotNull(configUnderTest);

            var connectionString = configUnderTest.GenerateMySqlConnectionString();
            Assert.Equal(@"server=(null);database=(null);uid=(null);pwd=(null);", connectionString);
        }

        [Fact]
        public void DbEnvironmentReadTestWithMissingVariablesTest()
        {
            var dbCredentials = new Dictionary<string, string>
            {
                {"MYSQL_SERVER", "localhost"},
                {"MYSQL_DATABASE", "JobAssistant"}
            };

            var configUnderTest = new DbEnvironmentConfig(dbCredentials);
            Assert.NotNull(configUnderTest);

            var connectionString = configUnderTest.GenerateMySqlConnectionString();
            Assert.Equal(@"server=localhost;database=JobAssistant;uid=(null);pwd=(null);", connectionString);
        }
    }
}
