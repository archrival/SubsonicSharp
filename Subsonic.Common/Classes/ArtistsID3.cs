using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class ArtistsID3
    {
        [XmlAttribute("ignoredArticles")]
        public string IgnoredArticles;

        [XmlElement("index")]
        public List<IndexID3> Index;
    }
}
