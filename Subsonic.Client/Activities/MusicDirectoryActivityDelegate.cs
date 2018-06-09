using Subsonic.Common.Classes;
using Subsonic.Common.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Subsonic.Client.Activities
{
    public class MusicDirectoryActivityDelegate<TImageType> : SubsonicActivityDelegate<Directory, TImageType> where TImageType : class, IDisposable
    {
        public MusicDirectoryActivityDelegate(string id)
        {
            Id = id;
        }

        private string Id { get; }

        public Func<CancellationToken?, Task<Directory>> CreateMethod(ISubsonicClient<TImageType> subsonicClient)
        {
            return cancelToken => subsonicClient.GetMusicDirectoryAsync(Id, cancelToken);
        }

        // Overrides for equality

        #region HashCode and Equality Overrides

        private const int HashFactor = 17;
        private const int HashSeed = 73; // Should be prime number
                                         // Should be prime number

        public static bool operator !=(MusicDirectoryActivityDelegate<TImageType> left, MusicDirectoryActivityDelegate<TImageType> right)
        {
            return !(left == right);
        }

        public static bool operator ==(MusicDirectoryActivityDelegate<TImageType> left, MusicDirectoryActivityDelegate<TImageType> right)
        {
            if (left is null)
                return right is null;

            if (right is null)
                return false;

            if (left.Id != null)
                if (!left.Id.Equals(right.Id))
                    return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            return obj != null && Equals(obj as MusicDirectoryActivityDelegate<TImageType>);
        }

        public override int GetHashCode()
        {
            var hash = HashSeed;
            hash = hash * HashFactor + typeof(MusicDirectoryActivityDelegate<TImageType>).GetHashCode();

            if (Id != null)
                hash = hash * HashFactor + Id.GetHashCode();

            return hash;
        }

        private bool Equals(MusicDirectoryActivityDelegate<TImageType> item)
        {
            return item != null && this == item;
        }

        #endregion HashCode and Equality Overrides
    }
}