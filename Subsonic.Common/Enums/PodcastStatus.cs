using System;
using System.Xml.Serialization;

namespace Subsonic.Common.Enums
{
    [Serializable]
    public enum PodcastStatus
    {
        [XmlEnum(Name = "new")]
        New,

        [XmlEnum(Name = "downloading")]
        Downloading,

        [XmlEnum(Name = "completed")]
        Completed,

        [XmlEnum(Name = "error")]
        Error,

        [XmlEnum(Name = "deleted")]
        Deleted,

        [XmlEnum(Name = "skipped")]
        Skipped,
    }
}
