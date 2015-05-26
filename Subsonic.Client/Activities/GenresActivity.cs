using Subsonic.Common.Classes;
using Subsonic.Common.Interfaces;

namespace Subsonic.Client.Activities
{
	public class GenresActivity<T> : SubsonicActivity<Genres, T>
	{
		public GenresActivity(ISubsonicClient<T> subsonicClient) : base(subsonicClient)
		{
			var activityDelegate = new SubsonicActivityDelegate<Genres, T> ();
			activityDelegate.Method = subsonicClient.GetGenresAsync;
			ActivityDelegate = activityDelegate;
		}
	}
}