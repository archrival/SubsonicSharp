using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class Songs
    {
        [XmlElement("song")]
        public List<Child> Items;
    }
}