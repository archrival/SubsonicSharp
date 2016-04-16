using System.Xml.Serialization;

namespace Subsonic.Common.Enums
{
    public enum SettingMethods
    {
        [XmlEnum("scanNow")]
        ScanNow,

        [XmlEnum("expunge")]
        Expunge
    }
}