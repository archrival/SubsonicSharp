using Subsonic.Client.Interfaces;
using Subsonic.Common.Interfaces;
using System;

namespace Subsonic.Client.Android
{
    public class SubsonicResponse<T> : Client.SubsonicResponse<T> where T : class, IDisposable
    {
        public SubsonicResponse(ISubsonicServer subsonicServer, IImageFormatFactory<T> imageFormatFactory) : base(subsonicServer, new SubsonicRequest<T>(subsonicServer, imageFormatFactory))
        {
        }
    }
}