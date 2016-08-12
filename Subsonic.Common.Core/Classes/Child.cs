using Subsonic.Common.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    [XmlInclude(typeof(PodcastEpisode))]
    [XmlInclude(typeof(NowPlayingEntry))]
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

        [Range(1, 5, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        [XmlAttribute("averageRating")]
        public double AverageRating;

        [XmlAttribute("bitRate")]
        public int BitRate;

        [XmlAttribute("bookmarkPosition")]
        public long BookmarkPosition;

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

        [XmlAttribute("originalHeight")]
        public int OriginalHeight;

        [XmlAttribute("originalWidth")]
        public int OriginalWidth;

        [XmlAttribute("parent")]
        public string Parent;

        [XmlAttribute("path")]
        public string Path;

        [XmlAttribute("playCount")]
        public long PlayCount;

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

        [Range(1, 5, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        [XmlAttribute("userRating")]
        public int UserRating;

        [XmlAttribute("year")]
        public int Year;

        public override bool Equals(object obj)
        {
            return obj != null && Equals(obj as Child);
        }

        private bool Equals(Child item)
        {
            return item != null && this == item;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            int hashFactor = 7;

            hash = (hash * hashFactor) + Id.GetHashCode();

            return hash;
        }

        public static bool operator ==(Child left, Child right)
        {
            if (ReferenceEquals(null, left))
                return ReferenceEquals(null, right);

            if (ReferenceEquals(null, right))
                return false;

            if (!string.Equals(left.Id, right.Id))
                return false;

            return true;
        }

        public static bool operator !=(Child left, Child right)
        {
            return !(left == right);
        }
    }
}