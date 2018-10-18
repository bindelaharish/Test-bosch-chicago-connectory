using Xunit;
using System.Collections.Generic;
using RB.JobAssistant.Data;
using RB.JobAssistant.Models;
using RB.JobAssistant.Models.Mapper;

namespace RB.JobAssistant.Tests.Models
{
    public class CategoryMappingTests
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void CopyJobObjectDataToCategoryModelTest()
        {
            var testCategory = new Category() { CategoryId = 123765, Name ="Product Group" };
            testCategory.Tools = new List<Tool> () { new Tool() { ToolId = 987456, Name = "Tool Name" }};
            var groupModel = JobAssistantMapper.Map<CategoryModel>(testCategory);
            Assert.NotNull(groupModel);
            Assert.Equal("Product Group", testCategory.Name);
            var enumerator = testCategory.Tools.GetEnumerator();
            enumerator.MoveNext();
            var onlyTool = enumerator.Current;
            Assert.NotNull(onlyTool);
            Assert.Equal("Tool Name", onlyTool.Name);
            Assert.Equal(987456, onlyTool.ToolId);
            enumerator.Dispose();
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void NullCategoryToCategoryGroupTest()
        {
            Job nullCategory = null;
            var categoryModel = JobAssistantMapper.Map<CategoryModel>(nullCategory);
            Assert.Null(categoryModel);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void EmptyCategoryToCategoryModelTest()
        {
            Category emptyCategory = new Category();
            var categoryModel = JobAssistantMapper.Map<CategoryModel>(emptyCategory);
            Assert.NotNull(categoryModel);
            Assert.True(string.IsNullOrEmpty(categoryModel.Name));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void CopyCategoryObjectDataToCategoryModelTest()
        {
            var testCategory = new Category
            {
                CategoryId = 123765,
                Name = "Category",
                Jobs = new List<Job>() {new Job() {JobId = 765456, Name = "Job Name"}}
            };
            var categoryModel = JobAssistantMapper.Map<CategoryModel>(testCategory);
            Assert.NotNull(categoryModel);
            Assert.Equal("Category", testCategory.Name);
            var enumerator = testCategory.Jobs.GetEnumerator();
            enumerator.MoveNext();
            var onlyJob = enumerator.Current;
            Assert.NotNull(onlyJob);
            Assert.Equal("Job Name", onlyJob.Name);
            Assert.Equal(765456, onlyJob.JobId);
            enumerator.Dispose();
        }
    }
}
