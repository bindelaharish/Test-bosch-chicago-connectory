using System.Linq;
using Microsoft.EntityFrameworkCore;
using RB.JobAssistant.Data;
using RB.JobAssistant.Data.Samples;
using Xunit;

namespace RB.JobAssistant.Tests.Data
{
    public class JobRelationshipTests
    {
        public JobRelationshipTests()
        {
            /**
             * This constructor is executed prior to each [Fact]-based unit test method.
             * Considering this, a seperate NAMED in-memory DB is initialized.
             */
            var dbId = RandomNumberHelper.NextInteger();
            _helper = new TestContextHelper("test_in-memory_DB-" + dbId);
            var context = new JobAssistantContext(_helper.Options);
            SampleBoschToolsDataSet.SeedBoschToolsGraphData(context);
        }

        private readonly TestContextHelper _helper;

        //  Categories of Trade -> Trade (Category) -> Jobs -> Materials -> Tools	

        [Fact]
        [Trait("Category", "Unit")]
        public void VerifyJobToMaterialsRelationshipsTest()
        {
            using (var context = new JobAssistantContext(_helper.Options))
            {
                var hammerAndHammerDrilJob = (from hammerJob in context.Jobs.Include(j => j.Materials)
                    where hammerJob.Name.Equals("Hammer & Hammer Drill")
                    select hammerJob).First();
                Assert.True(hammerAndHammerDrilJob.Materials.Count == 9);
                var hasMediumToqrueApplication = (from m in hammerAndHammerDrilJob.Materials where m.Name == "Plywood" select m).Count();
                Assert.True(hasMediumToqrueApplication == 1);
            }
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void LoadGraphAndVerifyFirstJobFirstMaterialAndFirstToolTest()
        {
            using (var context = new JobAssistantContext(_helper.Options))
            {
                var firstJob = context.Jobs.First();
                Assert.NotNull(firstJob);
                Assert.Equal(1, firstJob.JobId);

                var firstMaterial = context.Materials.First();
                Assert.NotNull(firstMaterial);
                Assert.True(firstMaterial.MaterialId > 0);

                var firstTool = context.Tools.First();
                Assert.NotNull(firstTool);
                Assert.True(firstTool.ToolId > 0);
            }
        }
    }
}