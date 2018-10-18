using System.Collections.Generic;
using RB.JobAssistant.Data;
using RB.JobAssistant.Models;
using RB.JobAssistant.Models.Mapper;
using Xunit;

namespace RB.JobAssistant.Tests.Models
{
    public class TradeMappingTests
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void CopyTradeObjectDataToTradeModelTest()
        {
            var testTrade = new Trade {TradeId = 123765, Name = "Trade"};
            testTrade.Tools = new List<Tool> {new Tool {ToolId = 112233, Name = "Tool Name"}};
            var tradeModel = JobAssistantMapper.Map<TradeModel>(testTrade);
            Assert.NotNull(tradeModel);
            Assert.Equal("Trade", testTrade.Name);
            var enumerator = testTrade.Tools.GetEnumerator();
            enumerator.MoveNext();
            var onlyTool = enumerator.Current;
            Assert.NotNull(onlyTool);
            Assert.Equal("Tool Name", onlyTool.Name);
            Assert.Equal(112233, onlyTool.ToolId);
            enumerator.Dispose();
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void EmptyTradeToTradeModelTest()
        {
            var emptyTrade = new Trade();
            var tradeModel = JobAssistantMapper.Map<TradeModel>(emptyTrade);
            Assert.NotNull(tradeModel);
            Assert.True(string.IsNullOrEmpty(tradeModel.Name));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void NullTradeToTradeGroupTest()
        {
            Job nullTrade = null;
            var tradeModel = JobAssistantMapper.Map<TradeModel>(nullTrade);
            Assert.Null(tradeModel);
        }
    }
}