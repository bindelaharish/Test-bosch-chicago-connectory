using System.Linq;
using RB.JobAssistant.Data;
using RB.JobAssistant.Repo;
using RB.JobAssistant.Tests.Data;
using Xunit;

namespace RB.JobAssistant.Tests.Repo
{
    public class AccessoryRepositoryTests
    {
        private readonly TestContextHelper _helper;
        public AccessoryRepositoryTests()
        {
            _helper = new TestContextHelper("test_in-memory_DB-" + RandomNumberHelper.NextInteger());
        }

        [Fact]
        public async void FilterAndContainsAccessoryTest()
        {
            using (var context = new JobAssistantContext(_helper.Options))
            {
                var repositoryUnderTest = new Repository(context);

                await repositoryUnderTest.Create(new Accessory {AccessoryId = 16001, Name = "Accessory Name " + 16001});
                var mySecondAccessory = new Accessory {AccessoryId = 17001, Name = "Accessory Name " + 17001};
                await repositoryUnderTest.Create(mySecondAccessory);
                await repositoryUnderTest.Create(new Accessory {AccessoryId = 18001, Name = "Accessory Name " + 18001});
                var myFourthAccessory = new Accessory {AccessoryId = 19001, Name = "Accessory Name " + 19001};
                await repositoryUnderTest.Create(myFourthAccessory);
                await repositoryUnderTest.Create(new Accessory {AccessoryId = 20001, Name = "Accessory Name " + 20001});

                int total;
                var accessories = repositoryUnderTest.Filter<Accessory>(j => j.AccessoryId > 16000 && j.AccessoryId < 18000, out total);
                Assert.True((from a in accessories where a.AccessoryId == 17001 select a).Count() == 1);
                // TODO: Assert.True((accessories.Select(a => a.AccessoryId == 17001).Count() == 1));
                Assert.True(total == 2);
                accessories = repositoryUnderTest.Filter<Accessory>(j => j.AccessoryId > 16000 && j.AccessoryId < 18000, out total, 1);
                Assert.True(total == 0);
                accessories = repositoryUnderTest.Filter<Accessory>(j => j.AccessoryId > 16000 && j.AccessoryId < 18000, out total, 0, 2);
                Assert.True((from a in accessories where a.AccessoryId == 17001 select a).Count() == 1);
                Assert.True(total == 2);
                accessories = repositoryUnderTest.Filter<Accessory>(j => j.AccessoryId >= 17001 && j.AccessoryId <= 20001, out total);
                Assert.True(total == 4);
                Assert.True((from a in accessories where a.AccessoryId == 17001 || a.AccessoryId == 19001 select a).Count() == 2);
            }
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async void RepositoryCreateTest()
        {
            using (var context = new JobAssistantContext(_helper.Options))
            {
                int nextId = RandomNumberHelper.NextInteger();
                var repositoryUnderTest = new Repository(context);

                var newAccessory = new Accessory()
                {
                    AccessoryId = nextId,
                    Name = "HCBG501T 5 pc.BlueGranite™ Turbo Carbide Hammer Drill Bits Set",
                    ModelNumber = "HCBG501T"
                };
                await repositoryUnderTest.Create(newAccessory);
                Assert.Equal("HCBG501T 5 pc.BlueGranite™ Turbo Carbide Hammer Drill Bits Set", context.Accessories.Single(c => c.AccessoryId == nextId).Name);
                await repositoryUnderTest.Delete(newAccessory);
                var verifiedAccessory = repositoryUnderTest.Single<Accessory>(c => c.AccessoryId == nextId);
                Assert.Null(verifiedAccessory);
            }
        }
    }
}
