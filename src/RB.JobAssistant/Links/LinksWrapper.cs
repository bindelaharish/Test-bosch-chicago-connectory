#pragma warning disable 1591
using System.Collections.Generic;

namespace RB.JobAssistant.Links
{
    public class LinksWrapper<T>
    {
        public T Value { get; set; }
        public List<LinkInfo> Links { get; set; }
    }
}
