using System.Xml.Serialization;
using System.Collections.Generic;

namespace Subsonic.Common.Classes
{
    public class SimilarSongs
    {
        [XmlElement("song")] public List<Child> Songs;
    }
}
