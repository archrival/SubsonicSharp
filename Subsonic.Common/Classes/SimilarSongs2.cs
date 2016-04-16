using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class SimilarSongs2
    {
        [XmlElement("song")]
        public List<Child> Songs;
    }
}