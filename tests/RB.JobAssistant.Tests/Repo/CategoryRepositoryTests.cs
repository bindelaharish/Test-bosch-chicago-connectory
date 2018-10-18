using System.Linq;
using System.Collections.Generic;
using RB.JobAssistant.Data;
using RB.JobAssistant.Repo;
using RB.JobAssistant.Tests.Data;
using Xunit;

namespace RB.JobAssistant.Tests.Repo
{
    public class CategoryRepositoryTests
    {
        private readonly TestContextHelper _helper;
        public CategoryRepositoryTests()
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

                var newCategory = repositoryUnderTest.Create<Category>(new Category { CategoryId = nextId, Name = "Test Category " + nextId }).Result;
                Assert.Equal("Test Category " + nextId, context.Categories.Single(c => c.CategoryId == nextId).Name);
                await repositoryUnderTest.Delete<Category>(newCategory);
                var verifyJob = repositoryUnderTest.Single<Category>(c => c.CategoryId == nextId);
                Assert.Null(verifyJob);
            }
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async void RepositoryCreateDeleteCategories()
        {
            using (var context = new JobAssistantContext(_helper.Options)) 
            {
                var repositoryUnderTest = new Repository(context);
                int firstId = RandomNumberHelper.NextInteger();
                var firstCategory = await repositoryUnderTest.Create<Category>(new Category { CategoryId = firstId, Name = "Test Category " + firstId }); // Was .Result
                int secondId = RandomNumberHelper.NextInteger();
                var secondCategory =  await repositoryUnderTest.Create<Category>(new Category { CategoryId = secondId, Name = "Test Category " + secondId }); // Was .Result

                Assert.Equal("Test Category " + firstId, context.Categories.Single(c => c.CategoryId == firstId).Name);
                Assert.Equal("Test Category " + secondId, context.Categories.Single(c => c.CategoryId == secondId).Name);

                await repositoryUnderTest.Delete<Category>(firstCategory);
                var verifyCategory = repositoryUnderTest.Single<Category>(c => c.CategoryId == firstCategory.CategoryId);
                Assert.Null(verifyCategory);

                await repositoryUnderTest.Delete<Category>(secondCategory);
                verifyCategory = repositoryUnderTest.Single<Category>(c => c.CategoryId == secondCategory.CategoryId);
                Assert.Null(verifyCategory);
            }
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void RepositoryCreateFetchListOfCategoriesTest()
        {
            using (var context = new JobAssistantContext(_helper.Options)) 
            {
                ICollection<int> verifySelectIds = new List<int>();
                var repositoryUnderTest = new Repository(context);
                for (int i = 0, nextId = RandomNumberHelper.NextInteger(); i < 10; i++) {
                    int categoryId = nextId + i;
                    if (i==4 || i == 6) {
                        verifySelectIds.Add(categoryId);
                    }
                    var testCategoryCreated = repositoryUnderTest.Create<Category>(new Category { CategoryId = categoryId, Name = "Test Category " + categoryId });
                    Assert.NotNull(testCategoryCreated);
                }
                context.SaveChanges();
 
                Assert.NotNull(context.Categories);
                var categoryCount = context.Categories.Count();
                Assert.True(categoryCount == 10);
                Assert.NotNull(context.Categories.First());
                Assert.NotNull(context.Categories.Last());

                var enumerator = verifySelectIds.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    int nextCategoryId = enumerator.Current;
                    var verifyCategory = repositoryUnderTest.Single<Category>(c => c.CategoryId == nextCategoryId);
                    Assert.NotNull(verifyCategory);
                }
                enumerator.Dispose();
            }
        }
    }
}
