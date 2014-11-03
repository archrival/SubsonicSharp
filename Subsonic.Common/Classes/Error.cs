using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class Error
    {
        [XmlAttribute("code")] public int Code;
        [XmlAttribute("message")] public string Message;
    }
}
