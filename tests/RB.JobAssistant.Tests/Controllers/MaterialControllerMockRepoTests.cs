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
    public class MaterialControllerMockRepoTests
    {
        public MaterialControllerMockRepoTests()
        {
            _logger = ApplicationLogging.CreateTypeLogger<MaterialsController>();
        }

        private readonly ILogger<MaterialsController> _logger;

        [Fact]
        public async Task CreateMaterialValidModelWithBadResult()
        {
            // Arrange & Act
            var mockRepo = new Mock<IRepository>();
            var controller = new MaterialsController(mockRepo.Object, _logger);

            var woodAndWoodComposities = new Material {Name = "Wood / Wood Composites"};
            mockRepo.Setup(repo => repo.Create(woodAndWoodComposities))
                .Returns(Task.FromResult(woodAndWoodComposities));

            // Act
            var materialModel = JobAssistantMapper.Map<MaterialModel>(woodAndWoodComposities);
            var result = await controller.CreateMaterial(materialModel);

            // Assert
            Assert.IsType<OkResult>(result);

            _logger.Log(LogLevel.Debug, 0, "Logging exeution of method CreateMaterialValidModelWithBadResult", null,
                null);
        }

        [Fact]
        public async Task UpdateMaterialValidModelWithOkResult()
        {
            // Arrange & Act
            var mockRepo = new Mock<IRepository>();
            var controller = new MaterialsController(mockRepo.Object, _logger);

            var fiberCementMaterial = new Material {MaterialId = 181818, Name = "Fiber / Cement"};
            mockRepo.Setup(repo => repo.Update(fiberCementMaterial)).Returns(Task.FromResult(181818));

            // Act
            var fiberCementModel = JobAssistantMapper.Map<MaterialModel>(fiberCementMaterial);
            var result = await controller.UpdateMaterial(181818, fiberCementModel);

            // Assert
            Assert.IsType<OkResult>(result);

            _logger.Log(LogLevel.Debug, 0, "Logging exeution of method UpdateMaterialValidModelWithOkResult", null,
                null);
        }
    }
}