using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RB.JobAssistant.Controllers;
using RB.JobAssistant.Data;
using RB.JobAssistant.Models;
using RB.JobAssistant.Models.Mapper;
using RB.JobAssistant.Repo;
using RB.JobAssistant.Util;
using Xunit;

namespace RB.JobAssistant.Tests.Controllers
{
    public class AccessoryControllerMockRepoTests
    {
        public AccessoryControllerMockRepoTests()
        {
            _logger = ApplicationLogging.CreateTypeLogger<AccessoriesController>();
        }

        private readonly ILogger<AccessoriesController> _logger;

        [Fact]
        public async Task CreateAccessoryValidModelWithCreatedResult()
        {
            // Arrange & Act
            var mockRepo = new Mock<IRepository>();
            var controller = new AccessoriesController(mockRepo.Object, _logger);

            var blackTapAndDieAccessorySet = new Accessory
            {
                AccessoryId = 123987,
                Name = "B44710 Black Oxide Tap and Die Set",
                ModelNumber = "B44710"
            };
            mockRepo.Setup(repo => repo.Create(blackTapAndDieAccessorySet))
                .Returns(Task.FromResult(blackTapAndDieAccessorySet));

            // Act
            var accessoryModel = JobAssistantMapper.Map<AccessoryModel>(blackTapAndDieAccessorySet);
            var result = await controller.CreateAccessory(accessoryModel);

            // Assert
            Assert.IsType<CreatedAtRouteResult>(result);

            _logger.Log(LogLevel.Debug, 0, "Logging exeution of method CreateAccessoryValidModelWithBadResult", null,
                null);
        }

        [Fact]
        public async Task CreateWithNullAndBadRequestObjectResult()
        {
            var mockRepo = new Mock<IRepository>();
            var controller = new AccessoriesController(mockRepo.Object, _logger);

            controller.ModelState.AddModelError("Error", "Null accessory object inputted");
            mockRepo.Setup(repo => repo.Create<Accessory>(null));

            // Act
            var result = await controller.CreateAccessory(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);

            _logger.Log(LogLevel.Debug, 0, "Logging exeution of method CreateWithNullAndBadRequestObjectResult", null,
                null);
        }

        [Fact]
        public async Task UpdateAccessoryValidModelWithOkResult()
        {
            // Arrange & Act
            var mockRepo = new Mock<IRepository>();
            var controller = new AccessoriesController(mockRepo.Object, _logger);

            var enhancedBlackTapAndDieAccessorySet = new Accessory
            {
                AccessoryId = 123987,
                Name = "B44710 Enhanced Black Oxide Tap and Die Set",
                ModelNumber = "B44710"
            };
            mockRepo.Setup(repo => repo.Update(enhancedBlackTapAndDieAccessorySet)).Returns(Task.FromResult(123987));

            // Act
            var accessoryModel = JobAssistantMapper.Map<AccessoryModel>(enhancedBlackTapAndDieAccessorySet);
            var result = await controller.UpdateAccessory(123987, accessoryModel);

            // Assert
            Assert.IsType<OkResult>(result);

            _logger.Log(LogLevel.Debug, 0, "Logging exeution of method UpdateAccessoryValidModelWithOkResult", null,
                null);
        }
    }
}