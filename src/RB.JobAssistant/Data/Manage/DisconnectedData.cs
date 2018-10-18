#pragma warning disable 1591
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace RB.JobAssistant.Data.Manage
{
    public class DisconnectedData {

        private readonly JobAssistantContext _context;

        public DisconnectedData(JobAssistantContext context) {
            _context = context;
        }

        public Category LoadCategoryAndJobsGraph(int id) {
            return _context.Categories.Include(c => c.Jobs).FirstOrDefault(c => c.CategoryId == id);
        }

        public Material LoadMaterialAndJobsGraph(int id)
        {
            return _context.Materials.FirstOrDefault(m => m.MaterialId == id);
        }

        public Job LoadJobandToolsGraph(int id) {
            return _context.Jobs.Include(j => j.ToolRelationships).FirstOrDefault(j => j.JobId == id);
        }
    }
}
