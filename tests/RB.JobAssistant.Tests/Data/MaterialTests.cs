using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RB.JobAssistant.Data;
using Xunit;

namespace RB.JobAssistant.Tests.Data
{
    public class MaterialTests : IClassFixture<DatabaseSetupTestFixture>
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void AddAndUpdateMaterialsWithIncludesAndTraversalTest()
        {
            var nextId = RandomNumberHelper.NextInteger();

            var parentCategory = new Category();
            parentCategory.CategoryId = nextId;
            parentCategory.Jobs = new List<Job>();

            var job = new Job {JobId = nextId, Name = "Test Job " + nextId};

            var material = new Material {MaterialId = nextId, Name = "Test Material " + nextId};
            job.Materials = new List<Material>();
            job.Materials.Add(material);

            parentCategory.Jobs.Add(job);

            var helper = new TestContextHelper("test_in-memory_DB-" + RandomNumberHelper.NextInteger());
            using (var context = new JobAssistantContext(helper.Options))
            {
                context.Add(parentCategory);
                var entitiesPeristed = context.SaveChanges();
                Assert.Equal(3, entitiesPeristed); // Expect 3 because a category, job and material are being persisted.
            }

            using (var context = new JobAssistantContext(helper.Options))
            {
                var myJob = context.Categories.Include(c => c.Jobs).ThenInclude(m => m.Materials)
                    .ThenInclude(t => t.Tools).Single(c => c.CategoryId == nextId).Jobs.Single(j => j.JobId == nextId);
                Assert.Equal("Test Job " + nextId, myJob.Name);
            }
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void AddAndUpdateMaterialsWithJobsDbSetQueryTest()
        {
            var nextId = RandomNumberHelper.NextInteger();

            var parentCategory = new Category();
            parentCategory.CategoryId = nextId;
            parentCategory.Name = "Test Category " + nextId;
            parentCategory.Jobs = new List<Job>();

            var job = new Job {JobId = nextId, Name = "Test Job " + nextId};

            var material = new Material {MaterialId = nextId, Name = "Test Material " + nextId};
            job.Materials = new List<Material>();
            job.Materials.Add(material);

            parentCategory.Jobs.Add(job);

            var helper = new TestContextHelper("test_in-memory_DB-" + RandomNumberHelper.NextInteger());
            using (var saveContext = new JobAssistantContext(helper.Options))
            {
                saveContext.Add(parentCategory);
                var entitiesPeristed = saveContext.SaveChanges();
                Assert.Equal(3, entitiesPeristed); // Expect 3 because a category, job and material are being persisted.
            }

            using (var queryContext = new JobAssistantContext(helper.Options))
            {
                var myJob = queryContext.Jobs.Include(j => j.Materials).Single(j => j.JobId == nextId);
                Assert.Equal("Test Job " + nextId, myJob.Name);
                Assert.True(myJob.Materials.Count == 1);
                var someMaterial = queryContext.Materials.Single(m => m.MaterialId == nextId);
                Assert.Equal("Test Material " + nextId, someMaterial.Name);
                var expectedCategoryName = "Test Category " + nextId;
                var topCategory = queryContext.Categories.Single(c => c.CategoryId == nextId);
                Assert.Equal(expectedCategoryName, topCategory.Name);
            }
        }
    }
}