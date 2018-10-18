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
    public class JobControllerMockRepoTests
    {
        public JobControllerMockRepoTests()
        {
            _logger = ApplicationLogging.CreateTypeLogger<JobsController>();
        }

        private readonly ILogger<JobsController> _logger;

        [Fact]
        public async Task CreateJobValidModelWithOkResult()
        {
            // Arrange & Act
            var mockRepo = new Mock<IRepository>();
            var controller = new JobsController(mockRepo.Object, _logger);

            var fastenJob = new Job {JobId = 14001, Name = "Fasten Job"};
            mockRepo.Setup(repo => repo.Create(fastenJob)).Returns(Task.FromResult(fastenJob));

            // Act
            var jobsModel = JobAssistantMapper.Map<JobModel>(fastenJob);
            var result = await controller.CreateJob(jobsModel);

            // Assert
            Assert.IsType<OkResult>(result);

            _logger.Log(LogLevel.Debug, 0, "Logging exeution of method CreateJobValidModelWithOkResult", null, null);
        }

        [Fact]
        public async Task CreateWithNullAndBadRequestObjectResult()
        {
            // Arrange & Act
            var mockRepo = new Mock<IRepository>();
            var controller = new JobsController(mockRepo.Object, _logger);
            controller.ModelState.AddModelError("Error", "Null job object inputted");
            mockRepo.Setup(repo => repo.Create<Job>(null));

            // Act
            var result = await controller.CreateJob(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);

            _logger.Log(LogLevel.Debug, 0, "Logging exeution of method CreateWithNullAndBadRequestObjectResult", null, null);
        }

        [Fact]
        public async Task UpdateJobValidModelWithOkResult()
        {
            // Arrange & Act
            var mockRepo = new Mock<IRepository>();
            var controller = new JobsController(mockRepo.Object, _logger);

            var fastenJob = new Job {JobId = 14001, Name = "Fasten No-slip Job"};
            mockRepo.Setup(repo => repo.Update(fastenJob)).Returns(Task.FromResult(14001));

            // Act
            var jobsModel = JobAssistantMapper.Map<JobModel>(fastenJob);
            var result = await controller.UpdateJob(14001, jobsModel);

            // Assert
            Assert.IsType<OkResult>(result);

            _logger.Log(LogLevel.Debug, 0, "Logging exeution of method UpdateJobValidModelWithOkResult", null, null);
        }

        [Fact]
        public async Task UpdateWithNullAndBadRequestObjectResult()
        {
            // Arrange & Act
            var mockRepo = new Mock<IRepository>();
            var controller = new JobsController(mockRepo.Object, _logger);
            controller.ModelState.AddModelError("Error", "Null job object inputted");
            mockRepo.Setup(repo => repo.Update<Job>(null));

            // Act
            var result = await controller.UpdateJob(-1, null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);

            _logger.Log(LogLevel.Debug, 0, "Logging exeution of method UpdateWithNullAndBadRequestResult", null, null);
        }
    }
}