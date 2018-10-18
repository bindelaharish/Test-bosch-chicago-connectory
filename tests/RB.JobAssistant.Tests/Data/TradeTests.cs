using RB.JobAssistant.Data;
using RB.JobAssistant.Data.Manage;
using Xunit;

namespace RB.JobAssistant.Tests.Data
{
    public class TradeTests
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void VerifyDatabaseInsertOfNewTrade()
        {
            var helper = new TestContextHelper("test_in-memory_DB-" + RandomNumberHelper.NextInteger());
            var context = new JobAssistantContext(helper.Options);
            var autoTrade = new Trade {Name = "Automotive and Other Vehicle Maintenance"};
            Assert.True(autoTrade.TradeId == 0);
            context.Trades.Add(autoTrade);
            var efDefaultId = autoTrade.TradeId;
            Assert.True(efDefaultId > 0);
            var savedCount = context.SaveChanges();
            Assert.True(savedCount == 1);
            Assert.True(autoTrade.TradeId > 0);
        }
    }
}