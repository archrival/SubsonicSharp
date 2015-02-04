using Subsonic.Common.Classes;
using Subsonic.Common.Interfaces;

namespace Subsonic.Client.Activities
{
    public sealed class MusicFoldersActivity<T> : SubsonicActivity<MusicFolders, T>
    {
        public MusicFoldersActivity(ISubsonicClient<T> subsonicClient) : base(subsonicClient)
        {
            Function = (ignored) => subsonicClient.GetMusicFoldersAsync();
        }
    }
}