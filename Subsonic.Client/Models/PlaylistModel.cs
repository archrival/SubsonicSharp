using Subsonic.Client.Tasks;
using Subsonic.Common.Classes;
using System;

namespace Subsonic.Client.Models
{
    public class PlaylistModel : ObservableObject
    {
        public TimeSpan Duration { get; set; }
        public string Name { get; set; }
        public Playlist Playlist { get; set; }
        public int Tracks { get; set; }

        public static bool operator !=(PlaylistModel left, PlaylistModel right)
        {
            return !(left == right);
        }

        public static bool operator ==(PlaylistModel left, PlaylistModel right)
        {
            if (left is null)
                return right is null;

            if (right is null)
                return false;

            if (!string.Equals(left.Name, right.Name))
                return false;

            if (!left.Tracks.Equals(right.Tracks))
                return false;

            if (!left.Duration.Equals(right.Duration))
                return false;

            if (!left.Playlist.Equals(right.Playlist))
                return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            return obj != null && Equals(obj as PlaylistModel);
        }

        public override int GetHashCode()
        {
            var hash = 13;
            var hashFactor = 7;

            hash = hash * hashFactor + base.GetHashCode();
            hash = hash * hashFactor + Name.GetHashCode();
            hash = hash * hashFactor + Tracks.GetHashCode();
            hash = hash * hashFactor + Duration.GetHashCode();
            hash = hash * hashFactor + Playlist.GetHashCode();

            return hash;
        }

        private bool Equals(PlaylistModel item)
        {
            return item != null && this == item;
        }
    }
}