using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class SongsByGenre
    {
        [XmlElement("song")]
        public List<Child> Songs;
    }
}