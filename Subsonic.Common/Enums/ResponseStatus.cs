using System.Xml.Serialization;

namespace Subsonic.Common.Enums
{
    public enum ResponseStatus
    {
        [XmlEnum("ok")]
        Ok,

        [XmlEnum("failed")]
        Failed
    }
}