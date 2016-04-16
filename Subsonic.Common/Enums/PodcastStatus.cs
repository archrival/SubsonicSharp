using System.Xml.Serialization;

namespace Subsonic.Common.Enums
{
    public enum PodcastStatus
    {
        [XmlEnum("new")]
        New,

        [XmlEnum("downloading")]
        Downloading,

        [XmlEnum("completed")]
        Completed,

        [XmlEnum("error")]
        Error,

        [XmlEnum("deleted")]
        Deleted,

        [XmlEnum("skipped")]
        Skipped
    }
}