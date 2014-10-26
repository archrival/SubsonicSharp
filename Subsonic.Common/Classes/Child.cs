using System;
using System.Xml.Serialization;
using Subsonic.Common.Enums;

namespace Subsonic.Common.Classes
{
    [XmlInclude(typeof(PodcastEpisode))]
    [XmlInclude(typeof(NowPlayingEntry))]
    [Serializable]
    public class Child
    {
        [XmlAttribute("album")]
        public string Album;

        [XmlAttribute("albumId")]
        public string AlbumId;

        [XmlAttribute("artist")]
        public string Artist;

        [XmlAttribute("artistId")]
        public string ArtistId;

        [XmlAttribute("averageRating")]
        public double AverageRating;

        [XmlAttribute("bitRate")]
        public int BitRate;

        [XmlAttribute("contentType")]
        public string ContentType;

        [XmlAttribute("coverArt")]
        public string CoverArt;

        [XmlAttribute("created")]
        public DateTime Created;

        [XmlAttribute("discNumber")]
        public int DiscNumber;

        [XmlAttribute("duration")]
        public int Duration;

        [XmlAttribute("genre")]
        public string Genre;

        [XmlAttribute("id")]
        public string Id;

        [XmlAttribute("isDir")]
        public bool IsDir;

        [XmlAttribute("isVideo")]
        public bool IsVideo;

        [XmlAttribute("parent")]
        public string Parent;

        [XmlAttribute("path")]
        public string Path;

        [XmlAttribute("size")]
        public long Size;

        [XmlAttribute("starred")]
        public DateTime Starred;

        [XmlAttribute("suffix")]
        public string Suffix;

        [XmlAttribute("title")]
        public string Title;

        [XmlAttribute("track")]
        public int Track;

        [XmlAttribute("transcodedContentType")]
        public string TranscodedContentType;

        [XmlAttribute("transcodedSuffix")]
        public string TranscodedSuffix;

        [XmlAttribute("type")]
        public MediaType Type;

        [XmlAttribute("userRating")]
        public int UserRating;

        [XmlAttribute("year")]
        public int Year;
    }
}
