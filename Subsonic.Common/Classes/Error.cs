using System;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    [Serializable]
    public class Error
    {
        [XmlAttribute("code")]
        public int Code;

        [XmlAttribute("message")]
        public string Message;
    }
}
