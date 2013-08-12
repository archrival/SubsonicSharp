using System;

namespace Subsonic.Client.Common
{
    public class SubsonicClient
    {
        public Uri ServerUrl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string ProxyServerUrl { get; set; }
        public int ProxyPort { get; set; }
        public string ProxyUserName { get; set; }
        public string ProxyPassword { get; set; }
        public Version ServerApiVersion { get; set; }
        public bool EncodePasswords { get; set; }
    }
}
