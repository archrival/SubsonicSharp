using System;
using Subsonic.Common.Classes;
using Subsonic.Client.Tasks;

namespace Subsonic.Client.Models
{
    public class PlaylistModel : ObservableObject
    {
        public string Name { get; set; }
        public int Tracks { get; set; }
        public TimeSpan Duration { get; set; }
        public Playlist Playlist { get; set; }

        public override bool Equals(object obj)
        {
            return obj != null && Equals(obj as PlaylistModel);
        }

        private bool Equals(PlaylistModel item)
        {
            return item != null && this == item;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            int hashFactor = 7;

            hash = (hash * hashFactor) + base.GetHashCode();
            hash = (hash * hashFactor) + Name.GetHashCode();
            hash = (hash * hashFactor) + Tracks.GetHashCode();
            hash = (hash * hashFactor) + Duration.GetHashCode();
            hash = (hash * hashFactor) + Playlist.GetHashCode();

            return hash;
        }

        public static bool operator ==(PlaylistModel left, PlaylistModel right)
        {
            if (ReferenceEquals(null, left))
                return ReferenceEquals(null, right);

            if (ReferenceEquals(null, right))
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

        public static bool operator !=(PlaylistModel left, PlaylistModel right)
        {
            return !(left == right);
        }
    }
}
