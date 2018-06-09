using Subsonic.Client.Extensions;
using Subsonic.Common.Classes;
using System;

namespace Subsonic.Client.Models
{
    public class TrackModel : ChildModel
    {
        private bool _cached;

        public string Album { get; set; }
        public int BitRate { get; set; }

        public bool Cached
        {
            get => _cached;
            set
            {
                if (_cached != value)
                {
                    _cached = value;
                    OnPropertyChanged();
                }
            }
        }

        public int DiscNumber { get; set; }
        public TimeSpan Duration { get; set; }
        public string FileName { get; set; }
        public Guid PlaylistGuid { get; set; }
        public TrackModel Source { get; set; }
        public string Title { get; set; }
        public int TrackNumber { get; set; }

        public static TrackModel Create(Child child, string fileName, bool cached)
        {
            return new TrackModel
            {
                Child = child,
                Artist = child.Artist,
                Duration = TimeSpan.FromSeconds(child.Duration),
                Genre = child.Genre,
                Title = child.Title,
                Album = child.Album,
                TrackNumber = child.Track,
                DiscNumber = child.DiscNumber,
                Year = child.Year,
                BitRate = child.BitRate,
                FileName = fileName,
                Cached = cached,
                Starred = child.Starred != new DateTime(),
                Rating = child.UserRating
            };
        }

        public static bool operator !=(TrackModel left, TrackModel right)
        {
            return !(left == right);
        }

        public static bool operator ==(TrackModel left, TrackModel right)
        {
            if (left is null)
                return right is null;

            if (right is null)
                return false;

            if (!left.PlaylistGuid.Equals(right.PlaylistGuid))
                return false;

            if (!string.Equals(left.Title, right.Title))
                return false;

            if (!string.Equals(left.Artist, right.Artist))
                return false;

            if (!string.Equals(left.Album, right.Album))
                return false;

            if (!string.Equals(left.FileName, right.FileName))
                return false;

            if (!left.DiscNumber.Equals(right.DiscNumber))
                return false;

            if (!left.TrackNumber.Equals(right.TrackNumber))
                return false;

            if (!left.Cached.Equals(right.Cached))
                return false;

            if (left.Source == null)
            {
                if (right.Source != null)
                    return false;
            }
            else
            {
                if (!left.Source.Equals(right.Source))
                    return false;
            }

            if (!left.Duration.Equals(right.Duration))
                return false;

            if (!left.BitRate.Equals(right.BitRate))
                return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            return obj != null && Equals(obj as TrackModel);
        }

        public override int GetHashCode()
        {
            var hash = 13;
            var hashFactor = 7;

            hash = hash * hashFactor + base.GetHashCode();
            hash = hash * hashFactor + PlaylistGuid.GetHashCode();
            hash = Title.GetHashCode(hash, hashFactor);
            hash = Artist.GetHashCode(hash, hashFactor);
            hash = Album.GetHashCode(hash, hashFactor);
            hash = FileName.GetHashCode(hash, hashFactor);
            hash = hash * hashFactor + DiscNumber.GetHashCode();
            hash = hash * hashFactor + TrackNumber.GetHashCode();
            hash = hash * hashFactor + Cached.GetHashCode();

            if (Source != null)
                hash = hash * hashFactor + Source.GetHashCode();

            hash = hash * hashFactor + Duration.GetHashCode();
            hash = hash * hashFactor + BitRate.GetHashCode();

            return hash;
        }

        private bool Equals(TrackModel item)
        {
            return item != null && this == item;
        }
    }
}