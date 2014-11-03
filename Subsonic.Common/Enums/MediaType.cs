using System.Xml.Serialization;

namespace Subsonic.Common.Enums
{
    public enum MediaType
    {
        [XmlEnum(Name = "music")] Music,
        [XmlEnum(Name = "podcast")] Podcast,
        [XmlEnum(Name = "audiobook")] Audiobook,
        [XmlEnum(Name = "video")] Video
    }
}
