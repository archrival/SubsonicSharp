using Subsonic.Common.Classes;
using Subsonic.Common.Interfaces;
using System;

namespace Subsonic.Client.Activities
{
    public class GenresActivity<TImageType> : SubsonicActivity<Genres, TImageType> where TImageType : class, IDisposable
    {
        public GenresActivity(ISubsonicClient<TImageType> subsonicClient)
        {
            ActivityDelegate = new SubsonicActivityDelegate<Genres, TImageType> { Method = subsonicClient.GetGenresAsync };
        }
    }
}