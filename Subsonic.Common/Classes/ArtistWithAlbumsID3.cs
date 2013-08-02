using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common
{
    [Serializable]
    public class ArtistWithAlbumsID3 : ArtistID3
    {
        [XmlElement("album")]
        public List<AlbumID3> Album;

    }
}
