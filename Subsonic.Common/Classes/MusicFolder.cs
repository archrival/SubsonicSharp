using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    [JsonObject("musicFolder")]
    public class MusicFolder
    {
        [JsonProperty("id")]
        [XmlAttribute("id")]
        public string Id;

        [JsonProperty("name")]
        [XmlAttribute("name")]
        public string Name;
    }
}