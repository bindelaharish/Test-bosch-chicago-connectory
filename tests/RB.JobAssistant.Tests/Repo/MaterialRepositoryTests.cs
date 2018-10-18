using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RB.JobAssistant.Data;
using RB.JobAssistant.Repo;
using RB.JobAssistant.Tests.Data;
using Xunit;

namespace RB.JobAssistant.Tests.Repo
{
    public class MaterialRepositoryTests
    {
        public MaterialRepositoryTests()
        {
            this._helper = new TestContextHelper("test_in-memory_DB-" + RandomNumberHelper.NextInteger());
        }

        private readonly TestContextHelper _helper;

        [Fact]
        [Trait("Category", "Unit")]
        public async void AddCategoryJobMaterialAndQueryTest()
        {
            var nextId = RandomNumberHelper.NextInteger();

            var parentCategory = new Category
            {
                CategoryId = nextId,
                Jobs = new List<Job>()
            };

            var job = new Job {JobId = nextId, Name = "Test Job " + nextId};

            var material = new Material {MaterialId = nextId, Name = "Test Material " + nextId};
            job.Materials = new List<Material> {material};

            parentCategory.Jobs.Add(job);

            using (var context = new JobAssistantContext(_helper.Options))
            {
                var repositoryUnderTest = new Repository(context);
                await repositoryUnderTest.Create(parentCategory);
                await repositoryUnderTest.SaveChanges();

                var myJob = repositoryUnderTest.All<Category>().Include(c => c.Jobs).ThenInclude(m => m.Materials)
                    .ThenInclude(t => t.Tools).Single(c => c.CategoryId == nextId).Jobs.Single(j => j.JobId == nextId);
                Assert.Equal("Test Job " + nextId, myJob.Name);
            }
        }
    }
}