using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class Directory
    {
        [XmlElement("child")]
        public List<Child> Child;

        [XmlAttribute("id")]
        public string Id;

        [XmlAttribute("name")]
        public string Name;

        [XmlAttribute("parent")]
        public string Parent;
    }
}
