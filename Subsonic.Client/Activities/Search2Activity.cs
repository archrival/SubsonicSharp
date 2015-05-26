using Subsonic.Client.Activities;
using Subsonic.Common.Classes;
using Subsonic.Common.Interfaces;

namespace Subsonic.Client
{
	public class Search2Activity<T> : SubsonicActivity<SearchResult2, T>
	{
		public Search2Activity(ISubsonicClient<T> subsonicClient, string query, int? artistCount = null, int? artistOffset = null, int? albumCount = null, int? albumOffset = null, int? songCount = null, int? songOffset = null, string musicFolderId = null) : base(subsonicClient)
		{
			var activityDelegate = new Search2ActivityDelegate<T>(query, artistCount, artistOffset, albumCount, albumOffset, songCount, songOffset, musicFolderId);
			activityDelegate.CreateFunction(subsonicClient);
			ActivityDelegate = activityDelegate;
		}
	}
}

