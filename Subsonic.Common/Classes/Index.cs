using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common
{
    [Serializable]
    public class Index
    {
        [XmlElement("artist")]
        public List<Artist> Artist;

        [XmlAttribute("name")]
        public string Name;
    }
}
