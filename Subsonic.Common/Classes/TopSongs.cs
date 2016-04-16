using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class TopSongs
    {
        [XmlElement("song")]
        public List<Child> Songs;
    }
}