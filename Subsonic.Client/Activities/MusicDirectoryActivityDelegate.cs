using System;
using System.Threading;
using System.Threading.Tasks;
using Subsonic.Common.Classes;
using Subsonic.Common.Interfaces;

namespace Subsonic.Client.Activities
{
    public class MusicDirectoryActivityDelegate<TImageType> : SubsonicActivityDelegate<Directory, TImageType>
    {
        private string Id { get; set; }

        public MusicDirectoryActivityDelegate(string id)
        {
            Id = id;
        }

        public Func<CancellationToken?, Task<Directory>> CreateMethod(ISubsonicClient<TImageType> subsonicClient)
        {
            return cancelToken => subsonicClient.GetMusicDirectoryAsync(Id, cancelToken);
        }

        // Overrides for equality
        #region HashCode and Equality Overrides
        private const int HashSeed = 73; // Should be prime number
        private const int HashFactor = 17; // Should be prime number

        public override int GetHashCode()
        {
            int hash = HashSeed;
            hash = (hash * HashFactor) + typeof(MusicDirectoryActivityDelegate<TImageType>).GetHashCode();

            if (Id != null)
                hash = (hash * HashFactor) + Id.GetHashCode();

            return hash;
        }

        public override bool Equals(object obj)
        {
            return obj != null && Equals(obj as MusicDirectoryActivityDelegate<TImageType>);
        }

        private bool Equals(MusicDirectoryActivityDelegate<TImageType> item)
        {
            return item != null && this == item;
        }

        public static bool operator ==(MusicDirectoryActivityDelegate<TImageType> left, MusicDirectoryActivityDelegate<TImageType> right)
        {
            if (ReferenceEquals(null, left))
                return ReferenceEquals(null, right);

            if (ReferenceEquals(null, right))
                return false;

            if (left.Id != null)
                if (!left.Id.Equals(right.Id))
                    return false;

            return true;
        }

        public static bool operator !=(MusicDirectoryActivityDelegate<TImageType> left, MusicDirectoryActivityDelegate<TImageType> right)
        {
            return !(left == right);
        }
        #endregion
    }
}

