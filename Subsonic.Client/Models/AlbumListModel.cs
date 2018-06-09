using Subsonic.Client.Tasks;
using Subsonic.Common.Enums;

namespace Subsonic.Client.Models
{
    public class AlbumListModel : ObservableObject
    {
        public AlbumListType AlbumListType { get; set; }
        public int Current { get; set; }

        public static bool operator !=(AlbumListModel left, AlbumListModel right)
        {
            return !(left == right);
        }

        public static bool operator ==(AlbumListModel left, AlbumListModel right)
        {
            if (left is null)
                return right is null;

            if (right is null)
                return false;

            if (!string.Equals(left.AlbumListType, right.AlbumListType))
                return false;

            return string.Equals(left.Current, right.Current);
        }

        public override bool Equals(object obj)
        {
            return obj != null && Equals(obj as AlbumListModel);
        }

        public override int GetHashCode()
        {
            var hash = 13;
            var hashFactor = 7;

            hash = hash * hashFactor + AlbumListType.GetHashCode();
            hash = hash * hashFactor + Current.GetHashCode();

            return hash;
        }

        private bool Equals(AlbumListModel item)
        {
            return item != null && this == item;
        }
    }
}