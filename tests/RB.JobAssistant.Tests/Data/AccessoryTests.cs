using RB.JobAssistant.Data;
using Xunit;

namespace RB.JobAssistant.Tests.Data
{
    public class AccessoryTests
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void VerifyDatabaseInsertOfAccessoryBit()
        {
            var helper = new TestContextHelper("test_in-memory_DB-" + RandomNumberHelper.NextInteger());
            var context = new JobAssistantContext(helper.Options);
            var bit = new Accessory
            {
                Name = "5/16 In. x 12 In. Bell Hanger Bits",
                ModelNumber = "BH1002",
                Attributes = "{ \"Weight\" : \"2.4 ounces\", \"Size\" = \"5/16-Inch\", \"Color\" = \"Blue\" }",
                SocialRating = 5M,
                MaterialNumber = "B000WA3M9E"
            };
            Assert.True(bit.AccessoryId == 0);
            context.Accessories.Add(bit);
            var efDefaultId = bit.AccessoryId; // Temporarily assigned by EF
            Assert.True(efDefaultId > 0);
            int savedCount = context.SaveChanges();
            Assert.True(savedCount == 1);
            Assert.True(bit.AccessoryId > 0); // New id is likely different than temporary id assigned above
        }
    }
}
