using System.Linq;
using Microsoft.EntityFrameworkCore;
using RB.JobAssistant.Data;
using RB.JobAssistant.Data.Samples;
using Xunit;

namespace RB.JobAssistant.Tests.Data
{
    public class ApplicationRelationshipTests
    {
        public ApplicationRelationshipTests()
        {
            /**
             * This constructor is executed prior to each [Fact]-based unit test method.
             * Considering this, a seperate NAMED in-memory DB is initialized.
             */
            var dbId = RandomNumberHelper.NextInteger();
            _helper = new TestContextHelper("test_in-memory_DB-" + dbId);
            _context = new JobAssistantContext(_helper.Options);
            /**
                Add sample data with ToolsSampleData.SeedBoschToolsGraphData(_context); 
                or using the subset with SeedBoschToolsSubsetData(_context).
             */
            SampleBoschToolsDataSet.SeedBoschToolsSubsetData(_context);
        }

        private readonly TestContextHelper _helper;

        private readonly JobAssistantContext _context;

        [Fact]
        public void VerifyApplicationToToolRelationshipTest()
        {
            var parentApplication = (from a in _context.Applications where a.Tag == "Hammer Drill Job with Concrete Material" select a).Include(a => a.ToolRelationships).First();
            var toolCount = parentApplication.ToolRelationships.Count; 
            Assert.Equal(4, toolCount);
        }
        
        [Fact]
        public void VerifyApplicationToAccessoryRelationshipTest()
        {
            var parentApplication = (from a in _context.Applications where a.Tag == "Drill & Drive Job with Chip Board Material" select a).Include(a => a.AccessoryRelationships).First();
            var accessoryCount = parentApplication.AccessoryRelationships.Count; 
            Assert.Equal(3, accessoryCount);
        }

    }
}
