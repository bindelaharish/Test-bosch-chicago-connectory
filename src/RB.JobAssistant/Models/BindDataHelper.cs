#pragma warning disable 1591
using System.Collections.Generic;
using RB.JobAssistant.Data;
using RB.JobAssistant.Repo;

namespace RB.JobAssistant.Models
{
    public class LateModelDataBinder
    {
        private readonly IRepository _repo;

        public LateModelDataBinder(IRepository repo)
        {
            _repo = repo;
        }

        public async void BindRelatedToolDataToModel(ICollection<ToolModel> toolModels)
        {
            if (toolModels != null)
                foreach (var toolModel in toolModels)
                {
                    var tool = await _repo.Find<Tool>(t => t.ToolId == toolModel.ToolId);
                    toolModel.Name = tool.Name;
                    toolModel.MaterialNumber = tool.MaterialNumber;
                    toolModel.ModelNumber = tool.ModelNumber;
                }
        }

        public async void BindRelatedAccessoryDataToModel(ICollection<AccessoryModel> accessoryModels)
        {
            if (accessoryModels != null)
                foreach (var accessoryModel in accessoryModels)
                {
                    var accessory = await _repo.Find<Accessory>(a => a.AccessoryId == accessoryModel.AccessoryId);
                    accessoryModel.Name = accessory.Name;
                    accessoryModel.MaterialNumber = accessory.MaterialNumber;
                    accessoryModel.ModelNumber = accessory.ModelNumber;
                }
        }
    }
}