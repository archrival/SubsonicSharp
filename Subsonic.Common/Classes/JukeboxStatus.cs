using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    [XmlInclude(typeof(JukeboxPlaylist))]
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