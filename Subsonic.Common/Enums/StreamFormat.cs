using System.Xml.Serialization;

namespace Subsonic.Common.Enums
{
    public enum StreamFormat
    {
        [XmlEnum("mp3")]
        Mp3,

        [XmlEnum("flv")]
        Flv,

        [XmlEnum("raw")]
        Raw
    }
}