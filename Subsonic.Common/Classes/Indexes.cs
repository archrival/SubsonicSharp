using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class Indexes
    {
        [XmlElement("child")]
        public List<Child> Children;

        [XmlAttribute("ignoredArticles")]
        public string IgnoredArticles;

        [XmlElement("index")]
        public List<Index> Items;

        [XmlAttribute("lastModified")]
        public long LastModified;

        [XmlElement("shortcut")]
        public List<Artist> Shortcuts;
    }
}