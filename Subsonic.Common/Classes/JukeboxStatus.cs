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

        [XmlIgnore]
        private int? _position;

        [XmlAttribute("position")]
        public int Position
        {
            get => _position.GetValueOrDefault();
            set => _position = value;
        }

        public bool ShouldSerializePosition()
        {
            return _position.HasValue;
        }
    }
}