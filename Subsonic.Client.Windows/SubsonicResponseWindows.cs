using Subsonic.Client.Interfaces;
using Subsonic.Common.Interfaces;
using System;

namespace Subsonic.Client.Windows
{
    public class SubsonicResponseWindows<T> : SubsonicResponse<T> where T : class, IDisposable
    {
        public SubsonicResponseWindows(ISubsonicServer subsonicServer, IImageFormatFactory<T> imageFormatFactory) : base(subsonicServer, new SubsonicRequestWindows<T>(subsonicServer, imageFormatFactory)) { }
    }
}