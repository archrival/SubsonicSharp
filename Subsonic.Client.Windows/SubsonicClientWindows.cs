using System;
using System.Drawing;

namespace Subsonic.Client.Windows
{
    public class SubsonicClientWindows : SubsonicClient<Image>
    {   
        public SubsonicClientWindows(Uri serverUrl, string userName, string password, string clientName) : base(serverUrl, userName, password, clientName)
        {
            var windowsResponse = new WindowsResponse<Image>(this);
            SubsonicResponse = windowsResponse;
            SubsonicRequest = windowsResponse.WindowsRequest;
        }

        public SubsonicClientWindows(Uri serverUrl, string userName, string password, Uri proxyServer, int proxyPort, string proxyUserName, string proxyPassword, string clientName) : base(serverUrl, userName, password, proxyServer, proxyPort, proxyUserName, proxyPassword, clientName)
        {
            var windowsResponse = new WindowsResponse<Image>(this);
            SubsonicResponse = windowsResponse;
            SubsonicRequest = windowsResponse.WindowsRequest;
        }
    }
}
