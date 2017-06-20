using System.Xml.Serialization;

namespace Subsonic.Common.Enums
{
    public enum JukeboxControlAction
    {
        [XmlEnum("get")]
        Get,

        [XmlEnum("status")]
        Status,

        [XmlEnum("set")]
        Set,

        [XmlEnum("start")]
        Start,

        [XmlEnum("stop")]
        Stop,

        [XmlEnum("skip")]
        Skip,

        [XmlEnum("add")]
        Add,

        [XmlEnum("clear")]
        Clear,

        [XmlEnum("remove")]
        Remove,

        [XmlEnum("shuffle")]
        Shuffle,

        [XmlEnum("setGain")]
        SetGain
    }
}