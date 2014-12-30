using System.Xml.Serialization;
using Subsonic.Common.Enums;

namespace Subsonic.Common.Classes
{
    public class Error
    {
        [XmlAttribute("code")] public ErrorCode Code;
        [XmlAttribute("message")] public string Message;
    }
}
