using Subsonic.Common.Classes;
using Subsonic.Common.Enums;
using Subsonic.Common.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Subsonic.Client.Activities
{
    public class AlbumListActivityDelegate<TImageType> : SubsonicActivityDelegate<AlbumList, TImageType> where TImageType : class, IDisposable
    {
        private AlbumListType AlbumListType { get; }
        private int? Size { get; }
        private int? Offset { get; }
        private int? FromYear { get; }
        private int? ToYear { get; }
        private string Genre { get; }
        private string MusicFolderId { get; }

        public AlbumListActivityDelegate(AlbumListType albumListType, int? size = null, int? offset = null, int? fromYear = null, int? toYear = null, string genre = null, string musicFolderId = null)
        {
            AlbumListType = albumListType;
            Size = size;
            Offset = offset;
            FromYear = fromYear;
            ToYear = toYear;
            Genre = genre;
            MusicFolderId = musicFolderId;
        }

        public Func<CancellationToken?, Task<AlbumList>> CreateMethod(ISubsonicClient<TImageType> subsonicClient)
        {
            return cancelToken => subsonicClient.GetAlbumListAsync(AlbumListType, Size, Offset, FromYear, ToYear, Genre, MusicFolderId, cancelToken);
        }

        // Overrides for equality

        #region HashCode and Equality Overrides

        private const int HashSeed = 73; // Should be prime number
        private const int HashFactor = 17; // Should be prime number

        public override int GetHashCode()
        {
            int hash = HashSeed;

            hash = (hash * HashFactor) + typeof(AlbumListActivityDelegate<TImageType>).GetHashCode();

            hash = (hash * HashFactor) + AlbumListType.GetHashCode();

            if (Size != null)
                hash = (hash * HashFactor) + Size.GetHashCode();

            if (Offset != null)
                hash = (hash * HashFactor) + Offset.GetHashCode();

            if (FromYear != null)
                hash = (hash * HashFactor) + FromYear.GetHashCode();

            if (ToYear != null)
                hash = (hash * HashFactor) + ToYear.GetHashCode();

            if (Genre != null)
                hash = (hash * HashFactor) + Genre.GetHashCode();

            if (MusicFolderId != null)
                hash = (hash * HashFactor) + MusicFolderId.GetHashCode();

            return hash;
        }

        public override bool Equals(object obj)
        {
            return obj != null && Equals(obj as AlbumListActivityDelegate<TImageType>);
        }

        private bool Equals(AlbumListActivityDelegate<TImageType> item)
        {
            return item != null && this == item;
        }

        public static bool operator ==(AlbumListActivityDelegate<TImageType> left, AlbumListActivityDelegate<TImageType> right)
        {
            if (ReferenceEquals(null, left))
                return ReferenceEquals(null, right);

            if (ReferenceEquals(null, right))
                return false;

            if (!left.AlbumListType.Equals(right.AlbumListType))
                return false;

            if (left.Size != null)
                if (!left.Size.Equals(right.Size))
                    return false;

            if (left.Offset != null)
                if (!left.Offset.Equals(right.Offset))
                    return false;

            if (left.FromYear != null)
                if (!left.FromYear.Equals(right.FromYear))
                    return false;

            if (left.ToYear != null)
                if (!left.ToYear.Equals(right.ToYear))
                    return false;

            if (left.Genre != null)
                if (!left.Genre.Equals(right.Genre))
                    return false;

            if (left.MusicFolderId != null)
                if (!left.MusicFolderId.Equals(right.MusicFolderId))
                    return false;

            return true;
        }

        public static bool operator !=(AlbumListActivityDelegate<TImageType> left, AlbumListActivityDelegate<TImageType> right)
        {
            return !(left == right);
        }

        #endregion HashCode and Equality Overrides
    }
}