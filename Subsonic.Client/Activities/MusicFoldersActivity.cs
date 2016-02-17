using Subsonic.Common.Classes;
using Subsonic.Common.Interfaces;
using System;

namespace Subsonic.Client.Activities
{
	public class MusicFoldersActivity<T> : SubsonicActivity<MusicFolders, T> where T : class, IDisposable
	{
		public MusicFoldersActivity(ISubsonicClient<T> subsonicClient)
		{
			ActivityDelegate = new SubsonicActivityDelegate<MusicFolders, T> { Method = subsonicClient.GetMusicFoldersAsync };
		}
	}
}