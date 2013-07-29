using System;
using System.Xml.Serialization;

namespace Subsonic.Common
{
    [Serializable]
    public class MusicFolder
    {
        [XmlAttribute("id")]
        public int Id;

        [XmlAttribute("name")]
        public string Name;
    }
}
