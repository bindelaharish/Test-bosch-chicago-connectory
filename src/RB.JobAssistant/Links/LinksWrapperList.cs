#pragma warning disable 1591
using System.Collections.Generic;

namespace RB.JobAssistant.Links
{
    public class LinksWrapperList<T>
    {
        public List<LinksWrapper<T>> Values { get; set; }
        public List<LinkInfo> Links { get; set; }
    }
}
