using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using RB.JobAssistant.Data;
using RB.JobAssistant.Repo;
using RB.JobAssistant.Tests.Data;
using Xunit;

namespace RB.JobAssistant.Tests.Repo
{
    public class JobCategoryRepositoryTests
    {
        private TestContextHelper helper;

        public JobCategoryRepositoryTests()
        {
            this.helper = new TestContextHelper("test_in-memory_DB-" + RandomNumberHelper.NextInteger());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async void RepositoryCreateCategoryAndJobTest()
        {
            int nextId = RandomNumberHelper.NextInteger();

            using (var context = new JobAssistantContext(helper.Options)) {
                var category = new Category { CategoryId = nextId, Name = "Test Category " + nextId };
                var job = new Job { JobId = nextId, Name = "Test Job " + nextId };
                category.Jobs = new List<Job>();
                category.Jobs.Add(job);

                var repositoryUnderTest = new Repository(context);
                await repositoryUnderTest.Create<Category>(category);
            }

            using (var context = new JobAssistantContext(helper.Options)) {
                var repositoryUnderTest = new Repository(context);
                var parentCategory = repositoryUnderTest.All<Category>().Include(c => c.Jobs).Single(c => c.CategoryId == nextId);
                Assert.NotNull(parentCategory);
                Assert.NotNull(parentCategory.Jobs);
                Assert.Equal(1, parentCategory.Jobs.Count);
                var childJob = parentCategory.Jobs.Single(j => j.JobId == nextId);
                Assert.Equal("Test Job " + nextId, childJob.Name);
                context.Remove(parentCategory);
                context.SaveChanges();
                var hasCategory = context.Categories.Any(c => c.CategoryId == nextId);
                Assert.False(hasCategory);
            }
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async void RepositoryCreateUpdateDeleteCategoryAndJobTests() {

            int categoryId = RandomNumberHelper.NextInteger();
            int jobId;

            using (var context = new JobAssistantContext(helper.Options)) {
                var repositoryUnderTest = new Repository(context);
                var category = new Category { CategoryId = categoryId, Name = "Test Category " + categoryId };
                jobId = RandomNumberHelper.NextInteger();
                var job = new Job { JobId = jobId, Name = "Test Job " + jobId };
                category.Jobs = new List<Job>();
                category.Jobs.Add(job);
                await repositoryUnderTest.Create<Category>(category);
            }

            using (var context = new JobAssistantContext(helper.Options)) {
                var repositoryUnderTest = new Repository(context);
                var parentCategory = repositoryUnderTest.All<Category>().Include(c => c.Jobs).Single(c => c.CategoryId == categoryId);
                Assert.NotNull(parentCategory);
                Assert.NotNull(parentCategory.Jobs);
                Assert.Equal(1, parentCategory.Jobs.Count);
                var jobToUpdate = parentCategory.Jobs.Single(j => j.JobId == jobId);
                jobToUpdate.Name = "Updated Test Job " + jobId;
                Assert.Equal("Updated Test Job " + jobId, parentCategory.Jobs.Single(j => j.JobId == jobId).Name);
                await repositoryUnderTest.Update(jobToUpdate);
                // TODO: Add assertion
                await repositoryUnderTest.Delete(parentCategory);
                var hasCategory = repositoryUnderTest.All<Category>().Any(c => c.CategoryId == categoryId);
                Assert.False(hasCategory);
            }
        }

        [Fact]
        public async void RepositoryCreateDeleteCategoryAndMultipleJobsTest() {
            int categoryId;
            using (var context = new JobAssistantContext(helper.Options)) {
                categoryId = RandomNumberHelper.NextInteger();
                var category = new Category { CategoryId = categoryId, Name = "Test Category " + categoryId };
                category.Jobs = new List<Job>();
                int jobId = RandomNumberHelper.NextInteger();
                var job1 = new Job { JobId = jobId, Name = "Test Job " + jobId };
                category.Jobs.Add(job1);
                jobId = RandomNumberHelper.NextInteger();
                var job2 = new Job { JobId = jobId, Name = "Test Job " + jobId };
                category.Jobs.Add(job2);

                var repositoryUnderTest = new Repository(context);
                var parentCategory = repositoryUnderTest.Create<Category>(category);
                Assert.Equal(2, category.Jobs.Count);        
            }

            using (var context = new JobAssistantContext(helper.Options)) {
                int initialCount = context.Categories.Count();
                var repositoryUnderTest = new Repository(context);
                var parentCategory = repositoryUnderTest.All<Category>().Include(c => c.Jobs).Single(c => c.CategoryId == categoryId);
                Assert.NotNull(parentCategory);
                Assert.NotNull(parentCategory.Jobs);
                await repositoryUnderTest.Delete(parentCategory);
                var hasCategory = repositoryUnderTest.All<Category>().Any(c => c.CategoryId == categoryId);
                Assert.False(hasCategory);
            }
        }
    }
}
