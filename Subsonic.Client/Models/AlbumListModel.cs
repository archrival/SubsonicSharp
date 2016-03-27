using Subsonic.Common.Enums;
using Subsonic.Client.Tasks;

namespace Subsonic.Client.Models
{
    public class AlbumListModel : ObservableObject
    {
        public AlbumListType AlbumListType { get; set; }
        public int Current { get; set; }

        public override bool Equals(object obj)
        {
            return obj != null && Equals(obj as AlbumListModel);
        }

        private bool Equals(AlbumListModel item)
        {
            return item != null && this == item;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            int hashFactor = 7;

            hash = (hash * hashFactor) + AlbumListType.GetHashCode();
            hash = (hash * hashFactor) + Current.GetHashCode();

            return hash;
        }

        public static bool operator ==(AlbumListModel left, AlbumListModel right)
        {
            if (ReferenceEquals(null, left))
                return ReferenceEquals(null, right);

            if (ReferenceEquals(null, right))
                return false;

            if (!string.Equals(left.AlbumListType, right.AlbumListType))
                return false;

            if (!string.Equals(left.Current, right.Current))
                return false;

            return true;
        }

        public static bool operator !=(AlbumListModel left, AlbumListModel right)
        {
            return !(left == right);
        }
    }
}