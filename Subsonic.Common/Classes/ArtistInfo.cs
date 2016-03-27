using System.Xml.Serialization;
using System.Collections.Generic;

namespace Subsonic.Common.Classes
{
    public class ArtistInfo : ArtistInfoBase
    {
        [XmlElement("similarArtist")] public List<Artist> SimilarArtists;
    }
}
