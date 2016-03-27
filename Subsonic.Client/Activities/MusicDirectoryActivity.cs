using Subsonic.Common.Classes;
using Subsonic.Common.Interfaces;
using System;

namespace Subsonic.Client.Activities
{
    public class MusicDirectoryActivity<TImageType> : SubsonicActivity<Directory, TImageType> where TImageType : class, IDisposable
    {
        public MusicDirectoryActivity(ISubsonicClient<TImageType> subsonicClient, string id)
        {
            var activityDelegate = new MusicDirectoryActivityDelegate<TImageType>(id);
            activityDelegate.Method = activityDelegate.CreateMethod(subsonicClient);
            ActivityDelegate = activityDelegate;
        }
    }
}

