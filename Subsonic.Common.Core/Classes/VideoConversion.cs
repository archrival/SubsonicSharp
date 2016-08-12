using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class VideoConversion
    {
        [XmlAttribute("id")]
        public string Id;

        [XmlAttribute("bitRate")]
        public int BitRate;

        [XmlAttribute("audioTrackId")]
        public int AudioTrackId;
    }
}