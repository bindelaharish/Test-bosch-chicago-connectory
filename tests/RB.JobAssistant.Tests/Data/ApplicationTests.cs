using RB.JobAssistant.Data;
using Xunit;

namespace RB.JobAssistant.Tests.Data
{
    public class ApplicationTests
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void VerifyDatabaseInsertOfNewApplication()
        {
            var helper = new TestContextHelper("test_in-memory_DB-" + RandomNumberHelper.NextInteger());
            var context = new JobAssistantContext(helper.Options);
            var application = new Application {Name = "Level"};
            Assert.True(application.ApplicationId == 0);
            context.Applications.Add(application);
            var efDefaultId = application.ApplicationId;
            Assert.True(efDefaultId > 0);
            var savedCount = context.SaveChanges();
            Assert.True(savedCount == 1);
            Assert.True(application.ApplicationId > 0);
        }
    }
}