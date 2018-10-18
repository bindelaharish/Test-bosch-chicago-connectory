using System;
using RB.JobAssistant.Data;

namespace RB.JobAssistant.Tests.Data
{
    public class DatabaseSetupTestFixture
    {
        protected JobAssistantContext databaseContext;

        public DatabaseSetupTestFixture()
        {
            var helper = new TestContextHelper("test" /* prod (or not "test" string */);
            databaseContext = new JobAssistantContext(helper.Options);
            try
            {
                databaseContext.Database.EnsureCreated();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception thrown during database schema creation: " + e);
            }
        }
    }

    public class DatabaseSetupTeardownTestFixture : DatabaseSetupTestFixture, IDisposable
    {
        public void Dispose()
        {
            databaseContext.Database.EnsureDeleted();
        }
    }
}