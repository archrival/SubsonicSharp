using Subsonic.Common.Classes;
using Subsonic.Common.Interfaces;
using System;

namespace Subsonic.Client.Activities
{
	public class MusicFoldersActivity<TImageType> : SubsonicActivity<MusicFolders, TImageType> where TImageType : class, IDisposable
	{
		public MusicFoldersActivity(ISubsonicClient<TImageType> subsonicClient)
		{
			ActivityDelegate = new SubsonicActivityDelegate<MusicFolders, TImageType> { Method = subsonicClient.GetMusicFoldersAsync };
		}
	}
}