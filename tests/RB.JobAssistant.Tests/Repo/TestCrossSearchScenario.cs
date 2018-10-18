using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using RB.JobAssistant.Data;
using RB.JobAssistant.Data.Samples;
using RB.JobAssistant.Repo;
using RB.JobAssistant.Tests.Data;
using Xunit;

namespace RB.JobAssistant.Tests.Repo
{
    public class TestCrossSearchScenario
    {
        private readonly TestContextHelper _helper;

        public TestCrossSearchScenario()
        {
            _helper = new TestContextHelper("test_in-memory_DB-" + RandomNumberHelper.NextInteger());
        }

        [Fact]
        public void TraversalOfJobsMaterialsApplicationsThenTools()
        {
            using (var context = new JobAssistantContext(_helper.Options))
            {
                SampleBoschToolsDataSet.SeedBoschToolsSubsetData(context);
                
                var repositoryUnderTest = new Repository(context);
                // PC: forward load all objects, OR, investigate if you can load per-level (and more on de-mand)
                var jobs = repositoryUnderTest.All<Job>().Include(j => j.Materials).ThenInclude(m => m.Applications)
                    .ThenInclude(a => a.ToolRelationships);
                Assert.NotNull(jobs);
                Assert.NotEmpty(jobs);

                var jobEnumerator = jobs.GetEnumerator();
                jobEnumerator.MoveNext();
                var firstjob = jobEnumerator.Current;
                Assert.NotNull(firstjob);

                // PC: Pick a material, then get a list of applications
                Debug.Assert(firstjob.Materials != null, "firstjob.Materials != null");
                var materialEnumerator = firstjob.Materials.GetEnumerator();
                materialEnumerator.MoveNext();
                var firstMaterial = materialEnumerator.Current;
                Assert.NotNull(firstMaterial);
                materialEnumerator.Dispose();
                
                // PC: Pick an Application, then get a list of tools
                var applicationEnumerator = firstMaterial.Applications.GetEnumerator();
                applicationEnumerator.MoveNext();
                var firstApplication = applicationEnumerator.Current;
                Assert.NotNull(firstApplication);
                applicationEnumerator.Dispose();
                
                // PC: Read the tool details, then assert
                var toolsEnumerator = firstApplication.ToolRelationships.GetEnumerator();
                toolsEnumerator.MoveNext();
                var firstTool = toolsEnumerator.Current;
                Assert.NotNull(firstTool);
                toolsEnumerator.Dispose();
            }                
        }
    }
}
