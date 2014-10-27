using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class Videos
    {
        [XmlElement("video")]
        public List<Child> Video;
    }
}
