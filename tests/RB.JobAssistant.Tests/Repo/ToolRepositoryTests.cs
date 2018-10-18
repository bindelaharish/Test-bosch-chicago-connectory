using System.Linq;
using RB.JobAssistant.Data;
using RB.JobAssistant.Repo;
using RB.JobAssistant.Tests.Data;
using Xunit;

namespace RB.JobAssistant.Tests.Repo
{
    public class ToolRepositoryTests
    {
        private readonly TestContextHelper _helper;
        public ToolRepositoryTests()
        {
            _helper = new TestContextHelper("test_in-memory_DB-" + RandomNumberHelper.NextInteger());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async void RepositoryCreateTest()
        {
            using (var context = new JobAssistantContext(_helper.Options))
            {
                int nextId = RandomNumberHelper.NextInteger();
                var repositoryUnderTest = new Repository(context);

                var newTool = new Tool()
                {
                    ToolId = nextId,
                    Name = "RH850VC 1 - 7 / 8 In.SDS - max® Rotary Hammer",
                    ModelNumber = "RH850VC"
                };
                await repositoryUnderTest.Create(newTool);
                Assert.Equal("RH850VC 1 - 7 / 8 In.SDS - max® Rotary Hammer", context.Tools.Single(t => t.ToolId == nextId).Name);
                await repositoryUnderTest.Delete(newTool);
                var verifiedTool = repositoryUnderTest.Single<Tool>(c => c.ToolId == nextId);
                Assert.Null(verifiedTool);
            }
        }
    }
}
