using System.Linq;
using Microsoft.EntityFrameworkCore;
using RB.JobAssistant.Data;
using RB.JobAssistant.Data.Manage;
using RB.JobAssistant.Data.Samples;
using Xunit;

namespace RB.JobAssistant.Tests.Data
{
    public class TradeRelationshipTests
    {
        public TradeRelationshipTests()
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
        public void VerifyTradeToCategoryRelationshipTest()
        {
            using (var context = new JobAssistantContext(_helper.Options))
            {
                var firstTrade = context.Trades.Include(t => t.Categories).First();
                Assert.NotNull(firstTrade);
                Assert.True(firstTrade.TradeId > 0);

                var associatedCategory = firstTrade.Categories.First();
                Assert.NotNull(associatedCategory);
                Assert.True(associatedCategory.CategoryId > 0);
            }
        }
    }
}