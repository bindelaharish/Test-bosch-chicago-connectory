#pragma warning disable 1591

namespace RB.JobAssistant.Data
{
    public class ApplicationAccessoryRelationship
    {
        public int ApplicationId { get; set; }

        public int AccessoryId { get; set; }

        // The equivalent [NotMapped] attribute is specified with the Fluent API.
        // TODO: public ICollection<Accessory> Accessories { get; }
        // TODO: Implement and add class constructor to use for read-only context.

        /*
            [NotMapped]
            public ICollection<Accessory> Accessories { get; }
         */
    }
}