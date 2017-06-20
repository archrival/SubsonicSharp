using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Subsonic.Common.Enums
{
    public enum ResponseStatus
    {
        [JsonProperty("ok")]
        [XmlEnum("ok")]
        Ok,

        [JsonProperty("failed")]
        [XmlEnum("failed")]
        Failed
    }
}