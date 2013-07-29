using System;
using System.Xml.Serialization;

namespace Subsonic.Common
{
    [Serializable]
    public class Artist
    {
        [XmlAttribute("id")]
        public string Id;

        [XmlAttribute("name")]
        public string Name;
    }
}
