using Subsonic.Client.Extensions;
using Subsonic.Client.Tasks;
using Subsonic.Common.Classes;

namespace Subsonic.Client.Models
{
    public class ChildModel : ObservableObject
    {
        public int AlbumArtSize { get; set; }
        public string Artist { get; set; }
        public Child Child { get; set; }
        public string CoverArt { get; set; }
        public string Genre { get; set; }
        public string Id { get; set; }
        public int Rating { get; set; }
        public bool Starred { get; set; }
        public int Year { get; set; }

        public static bool operator !=(ChildModel left, ChildModel right)
        {
            return !(left == right);
        }

        public static bool operator ==(ChildModel left, ChildModel right)
        {
            if (left is null)
                return right is null;

            if (right is null)
                return false;

            if (!string.Equals(left.Id, right.Id))
                return false;

            if (!string.Equals(left.Artist, right.Artist))
                return false;

            if (!string.Equals(left.Genre, right.Genre))
                return false;

            if (!left.Year.Equals(right.Year))
                return false;

            if (!left.Rating.Equals(right.Rating))
                return false;

            if (!left.Starred.Equals(right.Starred))
                return false;

            if (!left.AlbumArtSize.Equals(right.AlbumArtSize))
                return false;

            if (!left.CoverArt.Equals(right.CoverArt))
                return false;

            if (!left.Child.Equals(right.Child))
                return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            return obj != null && Equals(obj as ChildModel);
        }

        public override int GetHashCode()
        {
            var hash = 13;
            var hashFactor = 7;

            hash = Id.GetHashCode(hash, hashFactor);
            hash = Artist.GetHashCode(hash, hashFactor);
            hash = Genre.GetHashCode(hash, hashFactor);
            hash = hash * hashFactor + Year.GetHashCode();
            hash = hash * hashFactor + Rating.GetHashCode();
            hash = hash * hashFactor + Starred.GetHashCode();
            hash = hash * hashFactor + AlbumArtSize.GetHashCode();
            hash = CoverArt.GetHashCode(hash, hashFactor);
            hash = hash * hashFactor + Child.GetHashCode();

            return hash;
        }

        private bool Equals(ChildModel item)
        {
            return item != null && this == item;
        }
    }
}