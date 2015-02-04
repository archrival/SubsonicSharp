using Subsonic.Common.Classes;
using Subsonic.Common.Interfaces;

namespace Subsonic.Client.Activities
{
    public sealed class GenresActivity<T> : SubsonicActivity<Genres, T>
    {
        public GenresActivity(ISubsonicClient<T> subsonicClient) : base(subsonicClient)
        {
            Function = (ignored) => subsonicClient.GetGenresAsync();
        }
    }
}