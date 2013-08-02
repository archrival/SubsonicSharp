
using System.Xml.Serialization;

namespace Subsonic.Common
{
    /// <summary>
    /// Stream formats.
    /// </summary>
    public enum StreamFormat
    {
        [XmlEnum("mp3")]
        MP3,

        [XmlEnum("flv")]
        FLV,

        [XmlEnum("raw")]
        Raw,
    }
}
