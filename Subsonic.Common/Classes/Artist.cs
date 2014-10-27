using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class Artist
    {
        [XmlAttribute("id")]
        public string Id;

        [XmlAttribute("name")]
        public string Name;
    }
}
