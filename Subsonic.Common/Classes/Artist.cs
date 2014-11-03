using System.Xml.Serialization;
using System;

namespace Subsonic.Common.Classes
{
    public class Artist
    {
        [XmlAttribute("id")]
        public string Id;

        [XmlAttribute("name")]
        public string Name;

        [XmlAttribute("starred")]
        public DateTime Starred;
    }
}
