using Subsonic.Common.Enums;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class Error
    {
        [XmlAttribute("code")]
        public ErrorCode Code;

        [XmlAttribute("message")]
        public string Message;
    }
}