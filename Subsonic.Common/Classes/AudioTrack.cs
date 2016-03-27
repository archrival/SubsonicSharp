using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class AudioTrack
    {
        [XmlAttribute("id")] public string Id;
        [XmlAttribute("name")] public string Name;
        [XmlAttribute("languageCode")] public string LanguageCode;
    }
}