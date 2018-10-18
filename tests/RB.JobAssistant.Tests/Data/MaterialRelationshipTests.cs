using Microsoft.EntityFrameworkCore;
using RB.JobAssistant.Data;
using System.Linq;
using RB.JobAssistant.Data.Samples;
using Xunit;

namespace RB.JobAssistant.Tests.Data
{
    public class MaterialRelationshipTests
    {
        private readonly TestContextHelper _helper;
        public MaterialRelationshipTests()
        {
            /**
             * This constructor is executed prior to each [Fact]-based unit test method.
             * Considering this, a seperate NAMED in-memory DB is initialized.
             */
            int dbId = RandomNumberHelper.NextInteger();
            _helper = new TestContextHelper("test_in-memory_DB-" + dbId);
            var context = new JobAssistantContext(_helper.Options);
            SampleBoschToolsDataSet.SeedBoschToolsGraphData(context);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void VerifyMaterialToApplicationsRelationshipsTest()
        {
            using (var context = new JobAssistantContext(_helper.Options))
            {
                var laminatedWoodMaterial = (from hta in context.Materials.Include(m => m.Applications).Include(a => a.Applications)
                    where hta.MaterialId == 122
                                  select hta).First();
                Assert.True(laminatedWoodMaterial.Applications.Count == 3);
                var hasMediumTorqueApplication = (from a in laminatedWoodMaterial.Applications where a.Name == "Medium Torque Drive & Fasten" select a).Count();
                Assert.True(hasMediumTorqueApplication == 1);
            }
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void VerifyFirstMaterialAndFirstAssociatedChildToolTest()
        {
            using (var context = new JobAssistantContext(_helper.Options)) 
            {
                var firstMaterial = context.Materials.First();
                Assert.NotNull(firstMaterial);

                var firstTool = context.Tools.First();
                Assert.NotNull(firstTool);
            }
        }
        
        [Fact]
        [Trait("Category", "Unit")]
        public void LoadGraphAndVerifyFirstMaterialAndToolTest()
        {
            using (var context = new JobAssistantContext(_helper.Options))
            {
                var laminatedWoodMaterial = (from m in context.Materials.Include(m => m.Tools) where m.Name == "Laminated Wood / Composite Materials" select m).First();
                Assert.NotNull(laminatedWoodMaterial);
                Assert.True(laminatedWoodMaterial.MaterialId > 0);

                Assert.True(laminatedWoodMaterial.Tools.Count == 0);

                var newTool = new Tool() { ToolId = 123654 };
                laminatedWoodMaterial.Tools.Add(newTool);
                int lastSaveCount = context.SaveChanges();
                Assert.Equal(1, lastSaveCount);

                var tool123654 = (from t in context.Tools where t.ToolId == 123654 select t).First();
                Assert.NotNull(tool123654);
                Assert.Equal(123654, tool123654 .ToolId);
            }
        }
    }
}
