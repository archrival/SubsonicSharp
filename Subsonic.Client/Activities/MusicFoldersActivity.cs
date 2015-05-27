using Subsonic.Common.Classes;
using Subsonic.Common.Interfaces;

namespace Subsonic.Client.Activities
{
	public class MusicFoldersActivity<T> : SubsonicActivity<MusicFolders, T>
	{
		public MusicFoldersActivity(ISubsonicClient<T> subsonicClient)
		{
			ActivityDelegate = new SubsonicActivityDelegate<MusicFolders, T> { Method = subsonicClient.GetMusicFoldersAsync };
		}
	}
}