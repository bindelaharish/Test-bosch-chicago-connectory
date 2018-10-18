using System.Collections.Generic;
using System.Linq;
using RB.JobAssistant.Data;
using RB.JobAssistant.Models;
using RB.JobAssistant.Models.Mapper;
using Xunit;

namespace RB.JobAssistant.Tests.Models
{
    public class JobMappingTests
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void CopyJobObjectDataToJobModelTest()
        {
            var fastenJob = new Job {JobId = 1006, Name = "Fasten"};
            var fastenModel = JobAssistantMapper.Map<JobModel>(fastenJob);
            Assert.Equal("[ Fasten ]", fastenModel.Name);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void CopyJobsObjectDataToJobModelsTest()
        {
            var fastenJob = new Job { JobId = 1006, Name = "Fasten" };
            var routeJob = new Job {JobId = 1007, Name = "Route"};
            var grindJob = new Job { JobId = 1008, Name = "Grind" };

            var models = JobAssistantMapper.MapObjects<Job>(new List<Job>{ fastenJob, routeJob, grindJob });
            var fastenJobModel = models.First();
            Assert.Equal("Fasten", fastenJobModel.Name);
            var grindJobModel = models.Last();
            Assert.Equal("Grind", grindJobModel.Name);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void CopyJobsObjectDataToJobModelsAsQueryableTest()
        {
            var fastenJob = new Job { JobId = 1006, Name = "Fasten" };
            var routeJob = new Job { JobId = 1007, Name = "Route" };
            var grindJob = new Job { JobId = 1008, Name = "Grind" };

            var models = JobAssistantMapper.MapObjects<Job>((new List<Job> { fastenJob, routeJob, grindJob }).AsQueryable());
            var fastenJobModel = models.First();
            Assert.Equal("Fasten", fastenJobModel.Name);
            var grindJobModel = models.Last();
            Assert.Equal("Grind", grindJobModel.Name);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void CopyJobObjectDataToJobModelTestAgain()
        {
            var job = new Job {JobId = 987612365, Name = "Job Name"};
            job.Materials = new List<Material> {new Material {MaterialId = 123321, Name = "Material Name"}};
            var jobModel = JobAssistantMapper.Map<JobModel>(job);
            Assert.Equal(987612365, jobModel.JobId);
            Assert.Equal("[ Job Name ]", jobModel.Name);
            var enumerator = job.Materials.GetEnumerator();
            enumerator.MoveNext();
            var onlyMaterial = enumerator.Current;
            Assert.NotNull(onlyMaterial);
            Assert.Equal("Material Name", onlyMaterial.Name);
            enumerator.Dispose();
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void EmptyJobToJobModelTest()
        {
            var emptyJob = new Job();
            var jobModel = JobAssistantMapper.Map<JobModel>(emptyJob);
            Assert.NotNull(jobModel);
            Assert.True(string.IsNullOrEmpty(jobModel.Name));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void NullJobToJobModelTest()
        {
            Job nullJob = null;
            var jobModel = JobAssistantMapper.Map<JobModel>(nullJob);
            Assert.Null(jobModel);
        }
    }
}