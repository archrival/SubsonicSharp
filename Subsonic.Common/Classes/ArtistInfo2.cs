using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class ArtistInfo2 : ArtistInfoBase
    {
        [XmlElement("similarArtist")]
        public List<ArtistID3> SimilarArtists;
    }
}