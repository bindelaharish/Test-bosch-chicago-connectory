using RB.JobAssistant.Data;
using Xunit;

namespace RB.JobAssistant.Tests.Data
{
    public class CategoryTests
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void VerifyDatabaseInsertOfCategory()
        {
            var helper = new TestContextHelper("test_in-memory_DB-" + RandomNumberHelper.NextInteger());
            var context = new JobAssistantContext(helper.Options);
            var dremelMultiPurposeCategory = new Category()
            {
                Name = "Multi-Purpose"
            };
            Assert.True(dremelMultiPurposeCategory.CategoryId == 0);
            context.Categories.Add(dremelMultiPurposeCategory);
            var efDefaultId = dremelMultiPurposeCategory.CategoryId; // Temporarily assigned by EF
            Assert.True(efDefaultId > 0);
            int savedCount = context.SaveChanges();
            Assert.True(savedCount == 1);
            Assert.True(dremelMultiPurposeCategory.CategoryId > 0); // New id is likely different than temporary id assigned above
        }
    }
}
