using System.Xml.Serialization;
using System.Collections.Generic;

namespace Subsonic.Common.Classes
{
    public class ArtistInfo2 : ArtistInfoBase
    {
        [XmlElement("similarArtist")] public List<ArtistID3> SimilarArtist;
    }
}
