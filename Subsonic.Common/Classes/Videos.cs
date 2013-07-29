using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common
{
    [Serializable]
    public class Videos
    {
        [XmlElement("video")]
        public List<Child> Video;
    }
}
