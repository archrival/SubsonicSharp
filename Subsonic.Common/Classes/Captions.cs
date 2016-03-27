using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class Captions
    {
        [XmlAttribute("id")] public string Id;
        [XmlAttribute("name")] public string Name;
    }
}