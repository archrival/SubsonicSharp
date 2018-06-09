using Subsonic.Client.Tasks;

namespace Subsonic.Client.Models
{
    public class GenreModel : ObservableObject
    {
        public int AlbumCount { get; set; }
        public string Name { get; set; }
        public int SongCount { get; set; }

        public static bool operator !=(GenreModel left, GenreModel right)
        {
            return !(left == right);
        }

        public static bool operator ==(GenreModel left, GenreModel right)
        {
            if (left is null)
                return right is null;

            if (right is null)
                return false;

            if (!string.Equals(left.Name, right.Name))
                return false;

            if (!left.AlbumCount.Equals(right.AlbumCount))
                return false;

            return left.SongCount.Equals(right.SongCount);
        }

        public override bool Equals(object obj)
        {
            return obj != null && Equals(obj as GenreModel);
        }

        public override int GetHashCode()
        {
            var hash = 13;
            var hashFactor = 7;

            hash = hash * hashFactor + base.GetHashCode();
            hash = hash * hashFactor + Name.GetHashCode();
            hash = hash * hashFactor + AlbumCount.GetHashCode();
            hash = hash * hashFactor + SongCount.GetHashCode();

            return hash;
        }

        private bool Equals(GenreModel item)
        {
            return item != null && this == item;
        }
    }
}