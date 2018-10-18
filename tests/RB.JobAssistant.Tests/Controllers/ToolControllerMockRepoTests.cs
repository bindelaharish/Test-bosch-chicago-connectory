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
    public class ToolControllerMockRepoTests
    {
        public ToolControllerMockRepoTests()
        {
            _logger = ApplicationLogging.CreateTypeLogger<ToolsController>();
        }

        private readonly ILogger<ToolsController> _logger;

        [Fact]
        public async Task CreateWithNullAndBadRequestObjectResult()
        {
            // Arrange & Act
            var mockRepo = new Mock<IRepository>();
            var controller = new ToolsController(mockRepo.Object, _logger);
            controller.ModelState.AddModelError("Error", "Null job object inputted");
            mockRepo.Setup(repo => repo.Create<Tool>(null));

            // Act
            var result = await controller.CreateTool(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);

            _logger.Log(LogLevel.Debug, 0, "Logging exeution of method CreateWithNullAndBadRequestObjectResult", null, null);
        }

        [Fact]
        public async Task CreateToolValidModelWithCreatedResult()
        {
            // Arrange & Act
            var mockRepo = new Mock<IRepository>();
            var controller = new ToolsController(mockRepo.Object, _logger);

            var deepCutBandSawtool = new Tool {ToolId = 19101, Name = "Deep-Cut Band Saw"};
            mockRepo.Setup(repo => repo.Create(deepCutBandSawtool)).Returns(Task.FromResult(deepCutBandSawtool));

            // Act
            var bandSawToolModel = JobAssistantMapper.Map<ToolModel>(deepCutBandSawtool);
            var result = await controller.CreateTool(bandSawToolModel);

            // Assert
            Assert.IsType<StatusCodeResult>(result);

            _logger.Log(LogLevel.Debug, 0, "Logging exeution of method CreateToolValidModelWithOkResult", null, null);
        }
    }
}