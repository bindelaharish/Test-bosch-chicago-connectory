#pragma warning disable 1591
using System.Collections.Generic;
using System.Linq;

namespace RB.JobAssistant.Data.Manage {

    public class ToolQueryHelper
    {
        private readonly JobAssistantContext _context;

        public ToolQueryHelper(JobAssistantContext context)
        {
            _context = context;
        }

        public IEnumerable<Tool> FindTools(string namePattern)
        {
            List<Tool> tools = new List<Tool>();
            foreach (Category c in _context.Categories)
            {
                foreach (Job j in c.Jobs)
                {
                    foreach (Material m in j.Materials)
                    {
                        foreach (Tool t in m.Tools)
                        {
                            if (t.Name.Contains(namePattern))
                            {
                                tools.Add(t);
                            }

                        }
                    }
                }
            }
            return tools;
        }

        public IEnumerable<Tool> FindToolsViaJoin(string namePattern)
        {
            return 
            (from c in _context.Categories
             from j in c.Jobs
             from m in j.Materials
             from t in m.Tools
             where t.Name.Contains(namePattern) select t);
        }
    }
}
