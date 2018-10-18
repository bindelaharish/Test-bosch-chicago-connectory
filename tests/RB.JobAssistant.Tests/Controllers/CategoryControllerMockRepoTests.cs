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
    public class CategoryControllerMockRepoTests
    {
        public CategoryControllerMockRepoTests()
        {
            _logger = ApplicationLogging.CreateTypeLogger<CategoriesController>();
        }

        private readonly ILogger<CategoriesController> _logger;

        [Fact]
        public async Task CreateCategoryValidModelWithBadResult()
        {
            // Arrange & Act
            var mockRepo = new Mock<IRepository>();
            var controller = new CategoriesController(mockRepo.Object, _logger);
            controller.ModelState.AddModelError("Error", "Null category object inputted");
            mockRepo.Setup(repo => repo.Create<Category>(null));

            // Act
            var categoryModel = JobAssistantMapper.Map<CategoryModel>(null);
            var result = await controller.CreateCategory(categoryModel);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);

            _logger.Log(LogLevel.Debug, 0, "Logging exeution of method CreateCategoryValidModelWithBadResult", null,
                null);
        }

        [Fact]
        public async Task CreateCategoryValidModelWithOkResult()
        {
            // Arrange & Act
            var mockRepo = new Mock<IRepository>();
            var controller = new CategoriesController(mockRepo.Object, _logger);
            var aCategory = new Category {CategoryId = 12345, Name = "A test category"};
            mockRepo.Setup(repo => repo.Create(aCategory)).Returns(Task.FromResult(aCategory));

            // Act
            var categoryModel = JobAssistantMapper.Map<CategoryModel>(aCategory);
            var result = await controller.CreateCategory(categoryModel);

            // Assert
            Assert.IsType<OkResult>(result);

            _logger.Log(LogLevel.Debug, 0, "Logging exeution of method CreateCategoryValidModelWithOkResult", null,
                null);
        }

        [Fact]
        public async Task UpdateCategoryValidModelWithOkResult()
        {
            // Arrange & Act
            var mockRepo = new Mock<IRepository>();
            var controller = new CategoriesController(mockRepo.Object, _logger);

            var grindingAndSharpeningCategory = new Category {CategoryId = 998877, Name = "Grinding & Sharpening"};
            mockRepo.Setup(repo => repo.Update(grindingAndSharpeningCategory)).Returns(Task.FromResult(123987));

            // Act
            var accessoryModel = JobAssistantMapper.Map<CategoryModel>(grindingAndSharpeningCategory);
            var result = await controller.UpdateCategory(998877, accessoryModel);

            // Assert
            Assert.IsType<OkResult>(result);

            _logger.Log(LogLevel.Debug, 0, "Logging exeution of method UpdateCategoryValidModelWithOkResult", null,
                null);
        }
    }
}