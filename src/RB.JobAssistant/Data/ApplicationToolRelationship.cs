#pragma warning disable 1591

namespace RB.JobAssistant.Data
{
    public class ApplicationToolRelationship
    {
        public int ApplicationId { get; set; }

        public int ToolId { get; set; }

        // The equivalent [NotMapped] attribute is specified with the Fluent API.
        // TODO: public ICollection<Tool> Tools { get; }
        // TODO: Implement and add class constructor to use for read-only context.

        /*
            Possibly it most helpful for traversal to return all tools associated 
            to a tool, and to realize this as a method?

            [NotMapped]
            public ICollection<Tool> Tools { get; }
         */
    }
}