using System.Xml.Serialization;
using System.Collections.Generic;

namespace Subsonic.Common.Classes
{
    public class SimilarSongs2
    {
        [XmlElement("song")] public List<Child> Songs;
    }
}
