using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RB.JobAssistant.Controllers;
using RB.JobAssistant.Util;
using RB.JobAssistant.Data;
using RB.JobAssistant.Models;
using RB.JobAssistant.Models.Mapper;
using RB.JobAssistant.Repo;
using Xunit;

namespace RB.JobAssistant.Tests.Controllers
{
    public class TradeControllerMockRepoTests 
    {
        public TradeControllerMockRepoTests()
        {
            _logger = ApplicationLogging.CreateTypeLogger<TradesController>();
        }

        private readonly ILogger<TradesController> _logger;

        [Fact]
        public async Task CreateWithNullAndBadRequestObjectResult()
        {
            // Arrange & Act
            var mockRepo = new Mock<IRepository>();
            var controller = new TradesController(mockRepo.Object, _logger);
            controller.ModelState.AddModelError("Error", "Null job object inputted");
            mockRepo.Setup(repo => repo.Create<Trade>(null));

            // Act
            var result = await controller.CreateTrade(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);

            _logger.Log(LogLevel.Debug, 0, "Logging exeution of method CreateWithNullAndBadRequestObjectResult", null, null);
        }

        [Fact]
        public async Task CreateTradeValidModelWithOkResult()
        {
            // Arrange & Act
            var mockRepo = new Mock<IRepository>();
            var controller = new TradesController(mockRepo.Object, _logger);

            var automotiveTrade = new Trade {TradeId = 17123, Name = "Automotive and Other Vehicle Maintenance"};
            mockRepo.Setup(repo => repo.Create(automotiveTrade)).Returns(Task.FromResult(automotiveTrade));

            // Act
            var autoTradeModel = JobAssistantMapper.Map<TradeModel>(automotiveTrade);
            var result = await controller.CreateTrade(autoTradeModel);

            // Assert
            Assert.IsType<OkResult>(result);

            _logger.Log(LogLevel.Debug, 0, "Logging exeution of method CreateTradeValidModelWithOkResult", null, null);
        }
    }
}
