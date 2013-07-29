using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common
{
    [Serializable]
    public class SearchResult
    {
        [XmlElement("match")]
        public List<Child> Match;

        [XmlAttribute("offset")]
        public int Offset;

        [XmlAttribute("totalHits")]
        public int TotalHits;
    }
}
