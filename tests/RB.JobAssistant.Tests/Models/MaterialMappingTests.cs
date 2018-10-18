using System.Collections.Generic;
using RB.JobAssistant.Data;
using RB.JobAssistant.Models;
using RB.JobAssistant.Models.Mapper;
using Xunit;

namespace RB.JobAssistant.Tests.Models
{
    public class MaterialMappingTests
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void CopyMaterialObjectDataToMaterialModelTest()
        {
            var testMaterial = new Material() { MaterialId = 123765, Name = "Material" };
            testMaterial.Tools = new List<Tool>() { new Tool() { ToolId = 112233, Name = "Tool Name" } };
            var materialModel = JobAssistantMapper.Map<MaterialModel>(testMaterial);
            Assert.NotNull(materialModel);
            Assert.Equal("Material", testMaterial.Name);
            var enumerator = testMaterial.Tools.GetEnumerator();
            enumerator.MoveNext();
            var onlyTool = enumerator.Current;
            Assert.NotNull(onlyTool);
            Assert.Equal("Tool Name", onlyTool.Name);
            Assert.Equal(112233, onlyTool.ToolId);
            enumerator.Dispose();
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void NullMaterialToMaterialGroupTest()
        {
            Job nullMaterial = null;
            var materialModel = JobAssistantMapper.Map<MaterialModel>(nullMaterial);
            Assert.Null(materialModel);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void EmptyMaterialToMaterialModelTest()
        {
            Material emptyMaterial = new Material();
            var materialModel = JobAssistantMapper.Map<MaterialModel>(emptyMaterial);
            Assert.NotNull(materialModel);
            Assert.True(string.IsNullOrEmpty(materialModel.Name));
        }

    }
}
