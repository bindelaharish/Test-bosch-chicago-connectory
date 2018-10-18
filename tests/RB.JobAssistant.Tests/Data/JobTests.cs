using RB.JobAssistant.Data;
using Xunit;

namespace RB.JobAssistant.Tests.Data
{
    public class JobTests
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void VerifyDatabaseInsertOfJob()
        {
            var helper = new TestContextHelper("test_in-memory_DB-" + RandomNumberHelper.NextInteger());
            var context = new JobAssistantContext(helper.Options);
            var grindJob = new Job
            {
                Name = "Grind",
                DomainId = Tenant.CreateSingleTenant().DomainId
            };
            Assert.True(grindJob.JobId == 0);
            context.Jobs.Add(grindJob);
            var efDefaultId = grindJob.JobId; // Temporarily assigned by EF
            Assert.True(efDefaultId > 0);
            var savedCount = context.SaveChanges();
            Assert.True(savedCount == 1);
            Assert.True(grindJob.JobId > 0); // New id is likely different than temporary id assigned above
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void VerifyDatabaseInsertOfJobWithPowerToolsTenant()
        {
            var helper = new TestContextHelper("test_in-memory_DB-" + RandomNumberHelper.NextInteger());
            var context = new JobAssistantContext(helper.Options);
            var ptDomain = new Tenant {DomainId = "Power Tools"};
            context.Tenants.Add(ptDomain);

            var grindJob = new Job
            {
                Name = "Grind",
                DomainId = ptDomain.DomainId
            };
            Assert.True(grindJob.JobId == 0);
            context.Jobs.Add(grindJob);
            var efDefaultId = grindJob.JobId; // Temporarily assigned by EF
            Assert.True(efDefaultId > 0);
            var savedCount = context.SaveChanges();
            Assert.True(savedCount == 2);
            Assert.True(grindJob.JobId > 0); // New id is likely different than temporary id assigned above
        }
    }
}