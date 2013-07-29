using System;
using System.Xml.Serialization;

namespace Subsonic.Common
{
    [XmlInclude(typeof(JukeboxPlaylist))]
    [Serializable]
    public class JukeboxStatus
    {
        [XmlAttribute("currentIndex")]
        public int CurrentIndex;

        [XmlAttribute("gain")]
        public float Gain;

        [XmlAttribute("playing")]
        public bool Playing;

        [XmlAttribute("position")]
        public int Position;
    }
}
