﻿using Subsonic.Common.Classes;
using Subsonic.Common.Interfaces;

namespace Subsonic.Client.Activities
{
    public class MusicDirectoryActivity<TImageType> : SubsonicActivity<Directory, TImageType>
    {
        public MusicDirectoryActivity(ISubsonicClient<TImageType> subsonicClient, string id)
        {
            MusicDirectoryActivityDelegate<TImageType> activityDelegate = new MusicDirectoryActivityDelegate<TImageType>(id);
            activityDelegate.Method = activityDelegate.CreateMethod(subsonicClient);
            ActivityDelegate = activityDelegate;
        }
    }
}
