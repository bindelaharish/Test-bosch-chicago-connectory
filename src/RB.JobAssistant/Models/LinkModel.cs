namespace RB.JobAssistant.Models
{
    /// <summary>
    ///     Model to support navigation of HATEOS-based hyperlinks
    /// </summary>
    public class LinkModel
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="href"></param>
        public LinkModel(string name, string href)
        {
            Name = name;
            Href = href;
        }

        /// <summary>
        ///     Name
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Navigation reference
        /// </summary>
        public string Href { get; }
    }
}