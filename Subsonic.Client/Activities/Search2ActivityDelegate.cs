using Subsonic.Common.Classes;
using Subsonic.Common.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Subsonic.Client.Activities
{
    public class Search2ActivityDelegate<TImageType> : SubsonicActivityDelegate<SearchResult2, TImageType> where TImageType : class, IDisposable
    {
        public Search2ActivityDelegate(string query, int? artistCount = null, int? artistOffset = null, int? albumCount = null, int? albumOffset = null, int? songCount = null, int? songOffset = null, string musicFolderId = null)
        {
            Query = query;
            ArtistCount = artistCount;
            ArtistOffset = artistOffset;
            AlbumCount = albumCount;
            AlbumOffset = albumOffset;
            SongCount = songCount;
            SongOffset = songOffset;
            MusicFolderId = musicFolderId;
        }

        private int? AlbumCount { get; }
        private int? AlbumOffset { get; }
        private int? ArtistCount { get; }
        private int? ArtistOffset { get; }
        private string MusicFolderId { get; }
        private string Query { get; }
        private int? SongCount { get; }
        private int? SongOffset { get; }

        public Func<CancellationToken?, Task<SearchResult2>> CreateMethod(ISubsonicClient<TImageType> subsonicClient)
        {
            return cancelToken => subsonicClient.Search2Async(Query, ArtistCount, ArtistOffset, AlbumCount, AlbumOffset, SongCount, SongOffset, MusicFolderId, cancelToken);
        }

        // Overrides for equality

        #region HashCode and Equality Overrides

        private const int HashFactor = 17;
        private const int HashSeed = 73; // Should be prime number
                                         // Should be prime number

        public static bool operator !=(Search2ActivityDelegate<TImageType> left, Search2ActivityDelegate<TImageType> right)
        {
            return !(left == right);
        }

        public static bool operator ==(Search2ActivityDelegate<TImageType> left, Search2ActivityDelegate<TImageType> right)
        {
            if (left is null)
                return right is null;

            if (right is null)
                return false;

            if (left.Query != null)
                if (!left.Query.Equals(right.Query))
                    return false;

            if (left.MusicFolderId != null)
                if (!left.MusicFolderId.Equals(right.MusicFolderId))
                    return false;

            if (left.ArtistCount.HasValue)
                if (!right.ArtistCount.HasValue)
                    return false;
                else if (!left.ArtistCount.Value.Equals(right.ArtistCount.Value))
                    return false;

            if (left.ArtistOffset.HasValue)
                if (!right.ArtistOffset.HasValue)
                    return false;
                else if (!left.ArtistOffset.Value.Equals(right.ArtistOffset.Value))
                    return false;

            if (left.AlbumCount.HasValue)
                if (!right.AlbumCount.HasValue)
                    return false;
                else if (!left.AlbumCount.Value.Equals(right.AlbumCount.Value))
                    return false;

            if (left.AlbumOffset.HasValue)
                if (!right.AlbumOffset.HasValue)
                    return false;
                else if (!left.AlbumOffset.Value.Equals(right.AlbumOffset.Value))
                    return false;

            if (left.SongCount.HasValue)
                if (!right.SongCount.HasValue)
                    return false;
                else if (!left.SongCount.Value.Equals(right.SongCount.Value))
                    return false;

            if (left.SongOffset.HasValue)
                if (!right.SongOffset.HasValue)
                    return false;
                else if (!left.SongOffset.Value.Equals(right.SongOffset.Value))
                    return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            return obj != null && Equals(obj as Search2ActivityDelegate<TImageType>);
        }

        public override int GetHashCode()
        {
            var hash = HashSeed;
            hash = hash * HashFactor + typeof(Search2ActivityDelegate<TImageType>).GetHashCode();

            if (Query != null)
                hash = hash * HashFactor + Query.GetHashCode();

            if (MusicFolderId != null)
                hash = hash * HashFactor + MusicFolderId.GetHashCode();

            if (ArtistCount.HasValue)
                hash = hash * HashFactor + ArtistCount.Value.GetHashCode();

            if (ArtistOffset.HasValue)
                hash = hash * HashFactor + ArtistOffset.Value.GetHashCode();

            if (AlbumCount.HasValue)
                hash = hash * HashFactor + AlbumCount.Value.GetHashCode();

            if (AlbumOffset.HasValue)
                hash = hash * HashFactor + AlbumOffset.Value.GetHashCode();

            if (SongCount.HasValue)
                hash = hash * HashFactor + SongCount.Value.GetHashCode();

            if (SongOffset.HasValue)
                hash = hash * HashFactor + SongOffset.Value.GetHashCode();

            return hash;
        }

        private bool Equals(Search2ActivityDelegate<TImageType> item)
        {
            return item != null && this == item;
        }

        #endregion HashCode and Equality Overrides
    }
}