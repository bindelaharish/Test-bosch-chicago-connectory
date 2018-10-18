using RB.JobAssistant.Data;
using RB.JobAssistant.Data.Manage;
using Xunit;

namespace RB.JobAssistant.Tests.Data
{
    public class ToolTests
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void VerifyDatabaseInsertOfPowerTool()
        {
            var helper = new TestContextHelper("test_in-memory_DB-" + RandomNumberHelper.NextInteger());
            var context = new JobAssistantContext(helper.Options);
            var hammerDrill = new Tool
            {
                Name = "Variable Speed Hammer Drill Kit",
                ModelNumber = "1191VSRK",
                Attributes = "{ \"Weight\" : \"7.8 pounds\", \"Package Dimensions\" = \"14 x 11.9 x 4.3 inches\", \"Color\" = \"Blue\" }",
                MaterialNumber = "B000VZJGAO"
            };
            Assert.True(hammerDrill.ToolId == 0);
            context.Tools.Add(hammerDrill);
            var efDefaultId = hammerDrill.ToolId; // Temporarily assigned by EF
            Assert.True(efDefaultId > 0);
            int savedCount = context.SaveChanges();
            Assert.True(savedCount == 1);
            Assert.True(hammerDrill.ToolId > 0); // New id is likely different than temporary id assigned above
        }
    }
}
