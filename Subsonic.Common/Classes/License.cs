using System;
using System.Xml.Serialization;

namespace Subsonic.Common
{
    [Serializable]
    public class License
    {
        [XmlAttribute("date")]
        public DateTime Date;

        [XmlAttribute("email")]
        public string Email;

        [XmlAttribute("key")]
        public string Key;

        [XmlAttribute("valid")]
        public bool Valid;
    }
}
