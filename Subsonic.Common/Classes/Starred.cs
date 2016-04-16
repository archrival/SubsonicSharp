using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class Starred
    {
        [XmlElement("album")]
        public List<Child> Albums;

        [XmlElement("artist")]
        public List<Artist> Artists;

        [XmlElement("song")]
        public List<Child> Songs;
    }
}