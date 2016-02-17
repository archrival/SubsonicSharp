using System;
using Subsonic.Common.Classes;
using Subsonic.Common.Interfaces;

namespace Subsonic.Client.Activities
{
	public class GenresActivity<T> : SubsonicActivity<Genres, T> where T : class, IDisposable
	{
		public GenresActivity(ISubsonicClient<T> subsonicClient)
		{
		    ActivityDelegate = new SubsonicActivityDelegate<Genres, T> { Method = subsonicClient.GetGenresAsync };
		}
	}
}