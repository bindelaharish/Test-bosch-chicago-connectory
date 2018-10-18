#pragma warning disable 1591
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using RB.JobAssistant.Util;

namespace RB.JobAssistant.Data.Manage
{
    public class DbSchemaManager
    {
        private readonly ILogger<DbSchemaManager> _logger;
        protected JobAssistantContext DatabaseContext;

        public DbSchemaManager()
        {
            _logger = ApplicationLogging.CreateTypeLogger<DbSchemaManager>();
        }

        public void CreateSchema()
        {
            using (_logger.BeginScope(typeof(DbSchemaManager)))
            {
                var helper = new DbSchemaContextHelper("prod" /* production database instance */);
                DatabaseContext = new JobAssistantContext(helper.Options);
                try
                {
                    DatabaseContext.Database.EnsureCreated();
                    _logger.LogInformation("Successfully created table-based database schema for DB: {0}",
                        DatabaseContext.Database.GetDbConnection().Database);
                }
                catch (Exception e)
                {
                    _logger.LogError("Exception thrown during database schema creation: " + e);
                }
            }
        }

        public void DropSchema()
        {
            var helper = new DbSchemaContextHelper("prod");
            DatabaseContext = new JobAssistantContext(helper.Options);
            try
            {
                DatabaseContext.Database.EnsureDeleted();
                _logger.LogInformation("Successfully dropped or deleted schema via EF API mechanism: {0}.",
                    DatabaseContext.Database.GetDbConnection().Database);
            }
            catch (MySqlException sqlException)
            {
                _logger.LogError("Exception thrown during MySQL database schema drop or delete: " + sqlException);
            }
            catch (Exception e)
            {
                _logger.LogError("Exception thrown during database schema delete step: " + e);
            }
        }

        public void EraseAllData()
        {
            var helper = new DbSchemaContextHelper("prod");
            DatabaseContext = new JobAssistantContext(helper.Options);
            try
            {
                DatabaseContext.Database.ExecuteSqlCommand(
                    "DELETE FROM Accessories; DELETE FROM Tools; DELETE FROM Applications; DELETE FROM Materials; DELETE FROM Jobs; DELETE FROM Categories; DELETE FROM Trades;");
                _logger.LogInformation(
                    "Successfully erased ALL data from tables: 'Accessories', 'Tools', 'Applications', 'Materials', 'Jobs', 'Categories', 'Trades'");
            }
            catch (Exception exception)
            {
                _logger.LogError("Exception thrown during execution of SQL DELETE FROM: " + exception);
            }
        }

        public void LoadSampleData()
        {
            _logger.LogInformation("Erasing ALL previously inserted database data.");
            EraseAllData();
            _logger.LogInformation(
                "Inserting sample data into target database. (Check previous log entries for connection details.)");
            InsertSampleData();
        }

        private void InsertSampleData()
        {
            _logger.LogInformation("Seeding database with a subset of BOSCHTOOLS sample data.");
            DatabaseContext.LoadSampleData();
        }
    }

    internal class DbSchemaContextHelper
    {
        private readonly ILogger<DbSchemaContextHelper> _logger;

        public DbSchemaContextHelper(string environmentClass)
        {
            _logger = ApplicationLogging.CreateTypeLogger<DbSchemaContextHelper>();

            var optionsBuilder = new DbContextOptionsBuilder<JobAssistantContext>();
            if (!string.IsNullOrEmpty(environmentClass) && environmentClass.Contains("test"))
            {
                _logger.LogInformation("Setting-up for DB options for in-memory database.");
                optionsBuilder.UseInMemoryDatabase(environmentClass);
            }
            else
            {
                var helper = new DbServicesHelper(new AppSettingsConfig());
                var dbConnectionString = helper.GetDbConnectionString();
                _logger.LogInformation(
                    "Setting-up for DB options for external database accessed using connection string {0}.",
                    dbConnectionString);
                optionsBuilder.UseMySql(dbConnectionString);
            }

            Options = optionsBuilder.Options;
        }

        public DbContextOptions<JobAssistantContext> Options { get; }
    }
}