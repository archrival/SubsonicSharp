using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common
{
    [Serializable]
    public class Indexes
    {
        [XmlElement("child")]
        public List<Child> Child;

        [XmlElement("index")]
        public List<Index> Index;

        [XmlAttribute("lastModified")]
        public long LastModified;

        [XmlElement("shortcut")]
        public List<Artist> Shortcut;
    }
}
