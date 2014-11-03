using System.Xml.Serialization;

namespace Subsonic.Common.Enums
{
    public enum ResponseStatus
    {
        [XmlEnum(Name = "ok")] Ok,
        [XmlEnum(Name = "failed")] Failed
    }
}
