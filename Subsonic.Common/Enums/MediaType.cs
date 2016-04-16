using System.Xml.Serialization;

namespace Subsonic.Common.Enums
{
    public enum MediaType
    {
        [XmlEnum("music")]
        Music,

        [XmlEnum("podcast")]
        Podcast,

        [XmlEnum("audiobook")]
        Audiobook,

        [XmlEnum("video")]
        Video
    }
}