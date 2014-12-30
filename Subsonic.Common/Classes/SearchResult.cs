using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class SearchResult
    {
        [XmlElement("match")] public List<Child> Matches;
        [XmlAttribute("offset")] public int Offset;
        [XmlAttribute("totalHits")] public int TotalHits;
    }
}
