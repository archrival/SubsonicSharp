using System;
using System.Xml.Serialization;

namespace Subsonic.Common.Enums
{
    [Serializable]
    public enum ResponseStatus
    {
        [XmlEnum(Name = "ok")]
        Ok,

        [XmlEnum(Name = "failed")]
        Failed,
    }
}
