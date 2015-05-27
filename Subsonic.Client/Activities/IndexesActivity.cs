using Subsonic.Common.Classes;
using Subsonic.Common.Interfaces;

namespace Subsonic.Client.Activities
{
    public class IndexesActivity<TImageType> : SubsonicActivity<Indexes, TImageType>
    {
        public IndexesActivity(ISubsonicClient<TImageType> subsonicClient, int? musicFolderId = null, long? ifModifiedSince = null)
        {
            var activityDelegate = new IndexesActivityDelegate<TImageType>(musicFolderId, ifModifiedSince);
            activityDelegate.Method = activityDelegate.CreateMethod(subsonicClient);
            ActivityDelegate = activityDelegate;
        }
    }
}

