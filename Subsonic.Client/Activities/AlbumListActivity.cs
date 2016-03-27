using System;
using Subsonic.Common.Classes;
using Subsonic.Common.Interfaces;
using Subsonic.Common.Enums;

namespace Subsonic.Client.Activities
{
    public class AlbumListActivity<TImageType> : SubsonicActivity<AlbumList, TImageType> where TImageType : class, IDisposable
    {
        public AlbumListActivity(ISubsonicClient<TImageType> subsonicClient, AlbumListType albumListType, int? size = null, int? offset = null, int? fromYear = null, int? toYear = null, string genre = null, string musicFolderId = null)
        {
            var activityDelegate = new AlbumListActivityDelegate<TImageType>(albumListType, size, offset, fromYear, toYear, genre, musicFolderId);
            activityDelegate.Method = activityDelegate.CreateMethod(subsonicClient);
            ActivityDelegate = activityDelegate;
        }
    }
}