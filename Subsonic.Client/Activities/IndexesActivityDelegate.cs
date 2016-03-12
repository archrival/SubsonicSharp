using System;
using System.Threading;
using System.Threading.Tasks;
using Subsonic.Common.Classes;
using Subsonic.Common.Interfaces;

namespace Subsonic.Client.Activities
{
    public class IndexesActivityDelegate<TImageType> : SubsonicActivityDelegate<Indexes, TImageType> where TImageType : class, IDisposable
    {
        private int? MusicFolderId { get; }
        private long? IfModifiedSince { get; }

        public IndexesActivityDelegate(int? musicFolderId = null, long? ifModifiedSince = null)
        {
            MusicFolderId = musicFolderId;
            IfModifiedSince = ifModifiedSince;
        }

        public Func<CancellationToken?, Task<Indexes>> CreateMethod(ISubsonicClient<TImageType> subsonicClient)
        {
            return cancelToken => subsonicClient.GetIndexesAsync(MusicFolderId, IfModifiedSince, cancelToken);
        }

        // Overrides for equality
        #region HashCode and Equality Overrides

        private const int HashSeed = 73; // Should be prime number
        private const int HashFactor = 17; // Should be prime number

        public override int GetHashCode()
        {
            int hash = HashSeed;
            hash = (hash * HashFactor) + typeof(IndexesActivityDelegate<TImageType>).GetHashCode();
            
            if (MusicFolderId != null)
                hash = (hash * HashFactor) + MusicFolderId.GetHashCode();

            if (IfModifiedSince != null)
                hash = (hash * HashFactor) + IfModifiedSince.GetHashCode();
            
            return hash;
        }

        public override bool Equals(object obj)
        {
            return obj != null && Equals(obj as IndexesActivityDelegate<TImageType>);
        }

        private bool Equals(IndexesActivityDelegate<TImageType> item)
        {
            return item != null && this == item;
        }

        public static bool operator ==(IndexesActivityDelegate<TImageType> left, IndexesActivityDelegate<TImageType> right)
        {
            if (ReferenceEquals(null, left))
                return ReferenceEquals(null, right);

            if (ReferenceEquals(null, right))
                return false;

            if (left.MusicFolderId != null)
                if (!left.MusicFolderId.Equals(right.MusicFolderId))
                    return false;

            if (left.IfModifiedSince != null)
                if (!left.IfModifiedSince.Equals(right.IfModifiedSince))
                    return false;

            return true;
        }

        public static bool operator !=(IndexesActivityDelegate<TImageType> left, IndexesActivityDelegate<TImageType> right)
        {
            return !(left == right);
        }
        #endregion
    }
}

