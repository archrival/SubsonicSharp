using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class InternetRadioStation
    {
        [XmlAttribute("homePageUrl")]
        public string HomePageUrl;

        [XmlAttribute("id")]
        public string Id;

        [XmlAttribute("name")]
        public string Name;

        [XmlAttribute("streamUrl")]
        public string StreamUrl;
    }
}