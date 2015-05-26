using Subsonic.Common.Classes;
using Subsonic.Common.Interfaces;

namespace Subsonic.Client.Activities
{
	public class MusicFoldersActivity<T> : SubsonicActivity<MusicFolders, T>
	{
		public MusicFoldersActivity(ISubsonicClient<T> subsonicClient) : base(subsonicClient)
		{
			var activityDelegate = new SubsonicActivityDelegate<MusicFolders, T> ();
			activityDelegate.Method = subsonicClient.GetMusicFoldersAsync;
			ActivityDelegate = activityDelegate;
		}
	}
}