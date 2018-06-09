using Subsonic.Common.Classes;
using Subsonic.Common.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Subsonic.Client.Activities
{
    public class IndexesActivityDelegate<TImageType> : SubsonicActivityDelegate<Indexes, TImageType> where TImageType : class, IDisposable
    {
        public IndexesActivityDelegate(string musicFolderId = null, long? ifModifiedSince = null)
        {
            MusicFolderId = musicFolderId;
            IfModifiedSince = ifModifiedSince;
        }

        private long? IfModifiedSince { get; }
        private string MusicFolderId { get; }

        public Func<CancellationToken?, Task<Indexes>> CreateMethod(ISubsonicClient<TImageType> subsonicClient)
        {
            return cancelToken => subsonicClient.GetIndexesAsync(MusicFolderId, IfModifiedSince, cancelToken);
        }

        // Overrides for equality

        #region HashCode and Equality Overrides

        private const int HashFactor = 17;
        private const int HashSeed = 73; // Should be prime number
                                         // Should be prime number

        public static bool operator !=(IndexesActivityDelegate<TImageType> left, IndexesActivityDelegate<TImageType> right)
        {
            return !(left == right);
        }

        public static bool operator ==(IndexesActivityDelegate<TImageType> left, IndexesActivityDelegate<TImageType> right)
        {
            if (left is null)
                return right is null;

            if (right is null)
                return false;

            if (left.MusicFolderId != null)
                if (!left.MusicFolderId.Equals(right.MusicFolderId))
                    return false;

            if (left.IfModifiedSince != null)
                if (!left.IfModifiedSince.Equals(right.IfModifiedSince))
                    return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            return obj != null && Equals(obj as IndexesActivityDelegate<TImageType>);
        }

        public override int GetHashCode()
        {
            var hash = HashSeed;
            hash = hash * HashFactor + typeof(IndexesActivityDelegate<TImageType>).GetHashCode();

            if (MusicFolderId != null)
                hash = hash * HashFactor + MusicFolderId.GetHashCode();

            if (IfModifiedSince != null)
                hash = hash * HashFactor + IfModifiedSince.GetHashCode();

            return hash;
        }

        private bool Equals(IndexesActivityDelegate<TImageType> item)
        {
            return item != null && this == item;
        }

        #endregion HashCode and Equality Overrides
    }
}