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
    public class ApplicationControllerMockRepoTests
    {
        public ApplicationControllerMockRepoTests()
        {
            _logger = ApplicationLogging.CreateTypeLogger<ApplicationsController>();
        }

        private readonly ILogger<ApplicationsController> _logger;

        [Fact]
        public async Task CreateApplicationValidModelWithOkResult()
        {
            // Arrange & Act
            var mockRepo = new Mock<IRepository>();
            var controller = new ApplicationsController(mockRepo.Object, _logger);

            var drillApplication = new Application {ApplicationId = 16001, Name = "Drill & Drive"};
            mockRepo.Setup(repo => repo.Create(drillApplication)).Returns(Task.FromResult(drillApplication));

            // Act
            var jobsModel = JobAssistantMapper.Map<ApplicationModel>(drillApplication);
            var result = await controller.CreateApplication(jobsModel);

            // Assert
            Assert.IsType<OkResult>(result);

            _logger.Log(LogLevel.Debug, 0, "Logging exeution of method CreateApplicationValidModelWithOkResult", null,
                null);
        }

        [Fact]
        public async Task CreateWithNullAndBadRequestObjectResult()
        {
            // Arrange & Act
            var mockRepo = new Mock<IRepository>();
            var controller = new ApplicationsController(mockRepo.Object, _logger);
            controller.ModelState.AddModelError("Error", "Null job object inputted");
            mockRepo.Setup(repo => repo.Create<Job>(null));

            // Act
            var result = await controller.CreateApplication(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);

            _logger.Log(LogLevel.Debug, 0, "Logging exeution of method CreateApplicationValidModelWithBadResult", null,
                null);
        }

        [Fact]
        public async Task UpdateApplicationValidModelWithOkResult()
        {
            // Arrange & Act
            var mockRepo = new Mock<IRepository>();
            var controller = new ApplicationsController(mockRepo.Object, _logger);

            var straightAndSemiCurvedCutsApp =
                new Application {ApplicationId = 234987, Name = "Straight & Semi-Curved Cuts"};
            mockRepo.Setup(repo => repo.Update(straightAndSemiCurvedCutsApp)).Returns(Task.FromResult(234987));

            // Act
            var straightAndCurvedCutsAppModel =
                JobAssistantMapper.Map<ApplicationModel>(straightAndSemiCurvedCutsApp);
            var result = await controller.UpdateApplication(234987, straightAndCurvedCutsAppModel);

            // Assert
            Assert.IsType<OkResult>(result);

            _logger.Log(LogLevel.Debug, 0, "Logging exeution of method UpdateApplicationValidModelWithOkResult", null,
                null);
        }
    }
}