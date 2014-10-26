
using System.Xml.Serialization;

namespace Subsonic.Common.Enums
{
    /// <summary>
    /// Stream formats.
    /// </summary>
    public enum StreamFormat
    {
        [XmlEnum("mp3")]
        Mp3,

        [XmlEnum("flv")]
        Flv,

        [XmlEnum("raw")]
        Raw,
    }
}
