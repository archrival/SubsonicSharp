using Subsonic.Common.Classes;
using Subsonic.Common.Interfaces;
using System;

namespace Subsonic.Client.Activities
{
    public class Search2Activity<TImageType> : SubsonicActivity<SearchResult2, TImageType> where TImageType : class, IDisposable
    {
        public Search2Activity(ISubsonicClient<TImageType> subsonicClient, string query, int? artistCount = null, int? artistOffset = null, int? albumCount = null, int? albumOffset = null, int? songCount = null, int? songOffset = null, string musicFolderId = null)
        {
            var activityDelegate = new Search2ActivityDelegate<TImageType>(query, artistCount, artistOffset, albumCount, albumOffset, songCount, songOffset, musicFolderId);
            activityDelegate.Method = activityDelegate.CreateMethod(subsonicClient);
            ActivityDelegate = activityDelegate;
        }
    }
}