using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RB.JobAssistant.Data;
using RB.JobAssistant.Data.Manage;
using RB.JobAssistant.Repo;
using Xunit;

namespace RB.JobAssistant.Tests.Data
{
    public class ToolRelationshipExTests : IClassFixture<DatabaseSetupTestFixture>, IDisposable
    {
        public ToolRelationshipExTests()
        {
            context = new JobAssistantContext(helper.Options);
            SetupTestToolData();
        }

        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        private readonly TestContextHelper helper = new TestContextHelper("test_in-memory_DB-" + RandomNumberHelper.NextInteger());

        private readonly JobAssistantContext context;

        private Tool toolUnderTest;

        private void SetupTestToolData()
        {
            var toolId = RandomNumberHelper.NextInteger();
            var tool = new Tool {ToolId = toolId, Name = "Hammer Tool " + toolId};
            var materialId = RandomNumberHelper.NextInteger();
            var material = new Material
            {
                MaterialId = materialId,
                Name = "Material " + materialId,
                Tools = new List<Tool> {tool}
            };
            var jobId = RandomNumberHelper.NextInteger();
            var job = new Job
            {
                JobId = jobId,
                Name = "Job " + jobId,
                Materials = new List<Material> {material}
            };
            var categoryId = RandomNumberHelper.NextInteger();
            var category = new Category
            {
                CategoryId = categoryId,
                Name = "Category " + categoryId,
                Jobs = new List<Job> {job}
            };
            context.Add(category);
            context.SaveChanges();

            toolUnderTest = tool;
        }

        private bool disposedValue; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                    context.Remove(toolUnderTest);

                disposedValue = true;
            }
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async void AddCategoryJobMaterialToolAndQueryTest()
        {
            var nextId = RandomNumberHelper.NextInteger();

            var parentCategory = new Category
            {
                CategoryId = nextId,
                Jobs = new List<Job>()
            };

            var job = new Job {JobId = nextId, Name = "Test Job " + nextId};

            var material = new Material
            {
                MaterialId = nextId,
                Name = "Test Material " + nextId,
                Tools = new List<Tool> {new Tool {ToolId = nextId, Name = "Test Tool " + nextId}}
            };
            job.Materials = new List<Material> {material};

            parentCategory.Jobs.Add(job);

            var helper = new TestContextHelper("test_in-memory_DB-" + RandomNumberHelper.NextInteger());
            using (var otherContext = new JobAssistantContext(helper.Options))
            {
                var repositoryUnderCreateTest = new Repository(otherContext);
                await repositoryUnderCreateTest.Create<Category>(parentCategory);
                await repositoryUnderCreateTest.SaveChanges();

                var repositoryUnderQueryTest = new Repository(otherContext);
                var myJob = repositoryUnderQueryTest.All<Category>().Include(c => c.Jobs).ThenInclude(m => m.Materials)
                    .ThenInclude(t => t.Tools).Single(c => c.CategoryId == nextId).Jobs.Single(j => j.JobId == nextId);
                Assert.Equal("Test Job " + nextId, myJob.Name);

                var associatedTool = material.Tools.First();
                Assert.NotNull(associatedTool);
                Assert.Equal(nextId, associatedTool.ToolId);

                repositoryUnderCreateTest.Dispose();
                repositoryUnderQueryTest.Dispose();
            }
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void QueryForToolsTest()
        {
            var queryHelper = new ToolQueryHelper(context);
            var matchingResults = queryHelper.FindToolsViaJoin("Hammer");
            Assert.NotNull(matchingResults);
            var matchCount = matchingResults.Count();
            var hasMatch = false;
            var hammerToolMatch = string.Empty;
            foreach (var match in matchingResults)
            {
                hammerToolMatch = match.Name;
                if (toolUnderTest.Name == hammerToolMatch)
                {
                    hasMatch = true;
                    break;
                }
            }
            Assert.True(hasMatch);
        }
    }
}