using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class ArtistInfo : ArtistInfoBase
    {
        [XmlElement("similarArtist")]
        public List<Artist> SimilarArtists;
    }
}