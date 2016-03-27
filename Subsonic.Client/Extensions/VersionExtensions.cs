using System;

namespace Subsonic.Client.Extensions
{
    public static class VersionExtensions
    {
        public static Version Max(this Version version, Version newVersion)
        {
            return version > newVersion ? version : newVersion;
        }

        public static Version Min(this Version version, Version newVersion)
        {
            return version < newVersion ? version : newVersion;
        }
    }
}
