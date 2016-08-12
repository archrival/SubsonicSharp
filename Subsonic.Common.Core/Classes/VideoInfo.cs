using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class VideoInfo
    {
        [XmlElement("captions")]
        public List<Captions> Captions;

        [XmlElement("audioTrack")]
        public List<AudioTrack> AudioTrack;

        [XmlElement("conversion")]
        public List<VideoConversion> VideoConversion;

        [XmlAttribute("id")]
        public string Id;
    }
}