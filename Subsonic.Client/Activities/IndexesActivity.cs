﻿using Subsonic.Common.Classes;
using Subsonic.Common.Interfaces;
using System;

namespace Subsonic.Client.Activities
{
    public class IndexesActivity<TImageType> : SubsonicActivity<Indexes, TImageType> where TImageType : class, IDisposable
    {
        public IndexesActivity(ISubsonicClient<TImageType> subsonicClient, string musicFolderId = null, long? ifModifiedSince = null)
        {
            var activityDelegate = new IndexesActivityDelegate<TImageType>(musicFolderId, ifModifiedSince);
            activityDelegate.Method = activityDelegate.CreateMethod(subsonicClient);
            ActivityDelegate = activityDelegate;
        }
    }
}