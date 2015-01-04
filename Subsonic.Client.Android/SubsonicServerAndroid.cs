using System;
using System.Net;

namespace Subsonic.Client.Android
{
    public class SubsonicServerAndroid : SubsonicServer
    {
        public WebProxy Proxy { get; set; }

        public SubsonicServerAndroid(Uri serverUrl, string userName, string password, string clientName) : base(serverUrl, userName, password, clientName) { }

        public SubsonicServerAndroid(Uri serverUrl, string userName, string password, string clientName, string proxyServer, int proxyPort, string proxyUserName, string proxyPassword) : base (serverUrl, userName, password, clientName)
        {
            Proxy = new WebProxy(proxyServer, proxyPort);
            Proxy.Credentials = new NetworkCredential(proxyUserName, proxyPassword);
        }
    }
}

