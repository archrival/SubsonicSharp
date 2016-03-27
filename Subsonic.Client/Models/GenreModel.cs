using Subsonic.Client.Tasks;

namespace Subsonic.Client.Models
{
    public class GenreModel : ObservableObject
    {
        public string Name { get; set; }
        public int AlbumCount { get; set; }
        public int SongCount { get; set; }

        public override bool Equals(object obj)
        {
            return obj != null && Equals(obj as GenreModel);
        }

        private bool Equals(GenreModel item)
        {
            return item != null && this == item;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            int hashFactor = 7;

            hash = (hash * hashFactor) + base.GetHashCode();
            hash = (hash * hashFactor) + Name.GetHashCode();
            hash = (hash * hashFactor) + AlbumCount.GetHashCode();
            hash = (hash * hashFactor) + SongCount.GetHashCode();

            return hash;
        }

        public static bool operator ==(GenreModel left, GenreModel right)
        {
            if (ReferenceEquals(null, left))
                return ReferenceEquals(null, right);

            if (ReferenceEquals(null, right))
                return false;

            if (!string.Equals(left.Name, right.Name))
                return false;

            if (!left.AlbumCount.Equals(right.AlbumCount))
                return false;

            if (!left.SongCount.Equals(right.SongCount))
                return false;

            return true;
        }

        public static bool operator !=(GenreModel left, GenreModel right)
        {
            return !(left == right);
        }
    }
}