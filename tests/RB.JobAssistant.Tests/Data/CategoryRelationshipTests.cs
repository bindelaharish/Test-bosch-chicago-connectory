using System.Linq;
using Microsoft.EntityFrameworkCore;
using RB.JobAssistant.Data;
using RB.JobAssistant.Data.Samples;
using Xunit;

namespace RB.JobAssistant.Tests.Data
{
    public class CategoryRelationshipTests
    {
        public CategoryRelationshipTests()
        {
            /**
             * This constructor is executed prior to each [Fact]-based unit test method.
             * Considering this, a seperate NAMED in-memory DB is initialized.
             */
            var dbId = RandomNumberHelper.NextInteger();
            _helper = new TestContextHelper("test_in-memory_DB-" + dbId);
            var context = new JobAssistantContext(_helper.Options);
            SampleBoschToolsDataSet.SeedBoschToolTradesGraphData(context);
        }

        private readonly TestContextHelper _helper;

        [Fact]
        [Trait("Category", "Unit")]
        public void VerifyCategoryToCategoryRelationshipTest()
        {
            // Categories -> Category
            using (var context = new JobAssistantContext(_helper.Options))
            {
                var firstCategory = context.Categories.Include(c => c.Categories).First();
                Assert.NotNull(firstCategory);
                Assert.True(firstCategory.CategoryId > 0);
            }
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void VerifyTradeToCategoryToToolRelationshipsTest()
        {
            // Trades => Categories -> Tools
            using (var context = new JobAssistantContext(_helper.Options))
            {
                var trades = context.Trades.Include(t => t.Categories).ThenInclude(c => c.Categories).ToList();
                var firstTrade = trades.First();
                // TODO: Find a better way to trigger tools to be load. Namely, ThenInclude().
                // However, doing an Include() followed by a ToList() was a way to satisfy the test.
                // Possibly there is an issue with how ThenInclude() behaves when used with UseInMemoryDatabase().
                // See https://docs.microsoft.com/en-us/ef/core/querying/related-data for details.
                var alsoTools = context.Categories.Include(c => c.Tools).ToList();
                Assert.True(alsoTools.Count > 0);
                var firstCategory = firstTrade.Categories.First();
                Assert.NotNull(firstCategory);
                Assert.True(firstCategory.CategoryId > 0);
                var firstTool = firstCategory.Tools.First();
                Assert.True(firstTool.ToolId > 0);
            }
        }

    }
}