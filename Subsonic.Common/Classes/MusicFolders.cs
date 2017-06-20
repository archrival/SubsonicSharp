using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    [JsonObject("musicFolders")]
    public class MusicFolders
    {
        [JsonProperty("musicFolder")]
        [XmlElement("musicFolder")]
        public List<MusicFolder> Items;
    }
}