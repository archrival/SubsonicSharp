﻿using System;
using Subsonic.Common.Classes;
using Subsonic.Client.Extensions;

namespace Subsonic.Client.Models
{
    public class TrackModel : ChildModel
    {
        private bool _cached;

        public int DiscNumber { get; set; }
        public int TrackNumber { get; set; }
        public string Title { get; set; }
        public string Album { get; set; }
        public TimeSpan Duration { get; set; }
        public int BitRate { get; set; }
        public string FileName { get; set; }
        public Guid PlaylistGuid { get; set; }
        public TrackModel Source { get; set; }

        public bool Cached
        {
            get
            {
                return _cached;
            }
            set
            {
                if (_cached != value)
                {
                    _cached = value;
                    OnPropertyChanged();
                }
            }
        }

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

        public override bool Equals(object obj)
        {
            return obj != null && Equals(obj as TrackModel);
        }

        private bool Equals(TrackModel item)
        {
            return item != null && this == item;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            int hashFactor = 7;

            hash = (hash * hashFactor) + base.GetHashCode();
            hash = (hash * hashFactor) + PlaylistGuid.GetHashCode();
            hash = Title.GetHashCode(hash, hashFactor);
            hash = Artist.GetHashCode(hash, hashFactor);
            hash = Album.GetHashCode(hash, hashFactor);
            hash = FileName.GetHashCode(hash, hashFactor);
            hash = (hash * hashFactor) + DiscNumber.GetHashCode();
            hash = (hash * hashFactor) + TrackNumber.GetHashCode();
            hash = (hash * hashFactor) + Cached.GetHashCode();

            if (Source != null)
                hash = (hash * hashFactor) + Source.GetHashCode();

            hash = (hash * hashFactor) + Duration.GetHashCode();
            hash = (hash * hashFactor) + BitRate.GetHashCode();

            return hash;
        }

        public static bool operator ==(TrackModel left, TrackModel right)
        {
            if (ReferenceEquals(null, left))
                return ReferenceEquals(null, right);

            if (ReferenceEquals(null, right))
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

        public static bool operator !=(TrackModel left, TrackModel right)
        {
            return !(left == right);
        }
    }
}