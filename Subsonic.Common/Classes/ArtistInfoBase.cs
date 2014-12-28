using System.Xml.Serialization;
using System;

namespace Subsonic.Common.Classes
{
    public class ArtistInfoBase
    {
        [XmlElement("biography")] public string Biography;
        [XmlElement("musicBrainzId")] public string MusicBrainzId;
        [XmlElement("lastFmUrl")] public string LastFmUrl;
        [XmlElement("smallImageUrl")] public string SmallImageUrl;
        [XmlElement("mediumImageUrl")] public string MediumImageUrl;
        [XmlElement("largeImageUrl")] public string LargeImageUrl;
    }
}
