using System.Linq;
using RB.JobAssistant.Data;
using RB.JobAssistant.Repo;
using RB.JobAssistant.Tests.Data;
using Xunit;

namespace RB.JobAssistant.Tests.Repo
{
    public class JobRepositoryTests
    {
        public JobRepositoryTests()
        {
            _helper = new TestContextHelper("test_in-memory_DB-" + RandomNumberHelper.NextInteger());
        }

        private readonly TestContextHelper _helper;

        [Fact]
        public async void FilterAndContainsJobTest()
        {
            using (var context = new JobAssistantContext(_helper.Options))
            {
                var repositoryUnderTest = new Repository(context);

                await repositoryUnderTest.Create(new Job {JobId = 6001, Name = "Job Name " + 6001});
                var mySecondJob = new Job {JobId = 7001, Name = "Job Name " + 7001};
                await repositoryUnderTest.Create(mySecondJob);
                await repositoryUnderTest.Create(new Job {JobId = 8001, Name = "Job Name " + 8001});
                var myFourthJob = new Job {JobId = 9001, Name = "Job Name " + 9001};
                await repositoryUnderTest.Create(myFourthJob);
                await repositoryUnderTest.Create(new Job {JobId = 10001, Name = "Job Name " + 10001});

                int total;
                var jobs = repositoryUnderTest.Filter<Job>(j => j.JobId > 6000 && j.JobId < 8000, out total);
                Assert.True((from j in jobs where j.JobId == 7001 select j).Count() == 1);
                Assert.True(total == 2);
                jobs = repositoryUnderTest.Filter<Job>(j => j.JobId > 6000 && j.JobId < 8000, out total, 1);
                Assert.True(total == 0);
                jobs = repositoryUnderTest.Filter<Job>(j => j.JobId > 6000 && j.JobId < 8000, out total, 0, 2);
                Assert.True((from j in jobs where j.JobId == 7001 select j).Count() == 1);
                Assert.True(total == 2);
                jobs = repositoryUnderTest.Filter<Job>(j => j.JobId >= 7001 && j.JobId <= 10001, out total);
                Assert.True(total == 4);
                Assert.True((from j in jobs where j.JobId == 7001 || j.JobId == 10001 select j).Count() == 2);
            }
        }

        [Fact]
        public async void MultiPageJobResultsTest()
        {
            using (var context = new JobAssistantContext(_helper.Options))
            {
                var repositoryUnderTest = new Repository(context);

                for (var testId = 30000; testId < 30300; testId++)
                    await repositoryUnderTest.Create(new Job {JobId = testId, Name = "Job Name " + testId});

                int total;
                var pagedJobs = repositoryUnderTest.Filter(Job.IsValid(), out total, 1, 40);
                Assert.NotEmpty(pagedJobs);
                Assert.True(total == 40);

                pagedJobs = repositoryUnderTest.Filter(Job.IsValid(), out total, 3, 40);
                Assert.NotEmpty(pagedJobs);
                Assert.True(total == 40);
            }
        }

        [Fact]
        public async void PageJobResultsTest()
        {
            using (var context = new JobAssistantContext(_helper.Options))
            {
                var repositoryUnderTest = new Repository(context);

                await repositoryUnderTest.Create(new Job {JobId = 11001, Name = "Job Name " + 11001});
                var mySecondJob = new Job {JobId = 12001, Name = "Job Name " + 12001};
                await repositoryUnderTest.Create(mySecondJob);
                await repositoryUnderTest.Create(new Job {JobId = 13001, Name = "Job Name " + 13001});
                var myFourthJob = new Job {JobId = 14001, Name = "Job Name " + 14001};
                await repositoryUnderTest.Create(myFourthJob);
                await repositoryUnderTest.Create(new Job {JobId = 15001, Name = "Job Name " + 15001});

                int total;
                var jobs = repositoryUnderTest.Filter(Job.IsValid(), out total, 1, 2);
                Assert.True(total == 2);
                Assert.True((from j in jobs where j.JobId == 14001 select j).Count() == 1);
            }
        }

        [Fact]
        public async void RepositoryCreateDeleteJobTest()
        {
            using (var context = new JobAssistantContext(_helper.Options))
            {
                var repositoryUnderTest = new Repository(context);
                var nextId = RandomNumberHelper.NextInteger();
                var myJob = new Job {JobId = nextId, Name = "Job Name " + nextId};
                await repositoryUnderTest.Create(myJob);
                var verifyJob = repositoryUnderTest.Single<Job>(j => j.JobId == nextId);
                Assert.NotNull(verifyJob);
                await repositoryUnderTest.Delete(verifyJob);
                var nullJob = repositoryUnderTest.Single<Job>(j => j.JobId == nextId);
                Assert.Null(nullJob);
            }
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async void RepositoryCreateJobTest()
        {
            using (var context = new JobAssistantContext(_helper.Options))
            {
                var nextId = RandomNumberHelper.NextInteger();
                var repositoryUnderTest = new Repository(context);
                var myJob = new Job {JobId = nextId, Name = "Job Name " + nextId};
                await repositoryUnderTest.Create(myJob);
                var verifyJob = repositoryUnderTest.Single<Job>(j => j.JobId == nextId);
                Assert.NotNull(verifyJob);
            }
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async void RepositoryUpdateJobTest()
        {
            using (var context = new JobAssistantContext(_helper.Options))
            {
                var repositoryUnderTest = new Repository(context);
                var nextId = RandomNumberHelper.NextInteger();
                var myJob = new Job {JobId = nextId, Name = "Job Name " + nextId};
                await repositoryUnderTest.Create(myJob);
                var jobName = "Updated Job Name " + nextId;
                myJob.Name = jobName;
                var updateCount = await repositoryUnderTest.Update(myJob);
                Assert.True(updateCount == 1);
                var verifyJob = repositoryUnderTest.Single<Job>(j => j.JobId == nextId);
                Assert.NotNull(verifyJob);
                Assert.Equal(jobName, verifyJob.Name);
            }
        }

        [Fact]
        public async void SecondMultiPageJobResultsTest()
        {
            using (var context = new JobAssistantContext(_helper.Options))
            {
                var repositoryUnderTest = new Repository(context);

                for (var testId = 9300; testId < 9800; testId++)
                    await repositoryUnderTest.Create(new Job {JobId = testId, Name = "Job Name " + testId});

                int total;
                var pagedJobs = repositoryUnderTest.Filter(Job.IsValid(), out total, 1, 50);
                Assert.NotEmpty(pagedJobs);
                Assert.True(total == 50);

                pagedJobs = repositoryUnderTest.Filter(Job.IsValid(), out total, 2, 50);
                Assert.NotEmpty(pagedJobs);
                Assert.True(total == 50);

                pagedJobs = repositoryUnderTest.Filter(Job.IsValid(), out total, 3, 50);
                Assert.NotEmpty(pagedJobs);
                Assert.True(total == 50);

                pagedJobs = repositoryUnderTest.Filter(Job.IsValid(), out total, 4, 50);
                Assert.NotEmpty(pagedJobs);
                Assert.True(total == 50);

                pagedJobs = repositoryUnderTest.Filter(Job.IsValid(), out total, 5, 50);
                Assert.NotEmpty(pagedJobs);
                Assert.True(total == 50);

                pagedJobs = repositoryUnderTest.Filter(Job.IsValid(), out total, 6, 50);
                Assert.NotEmpty(pagedJobs);
                Assert.True(total == 50);
            }
        }
    }
}