using Subsonic.Client.Interfaces;
using System;
using System.Net;

namespace Subsonic.Client
{
    public class SubsonicServer : ISubsonicServer
    {
        Uri Url { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        string ClientName { get; set; }
        Version ApiVersion { get; set; }
        IWebProxy Proxy { get; set; }

        public SubsonicServer(Uri serverUrl, string userName, string password, string clientName)
        {
            Url = serverUrl;
            UserName = userName;
            Password = password;
            ClientName = clientName;
        }

        public SubsonicServer(Uri serverUrl, string userName, string password, string clientName, string proxyServer, int proxyPort) : this(serverUrl, userName, password, clientName)
        {
            Proxy = new WebProxy(proxyServer, proxyPort);
        }

        public SubsonicServer(Uri serverUrl, string userName, string password, string clientName, string proxyServer, int proxyPort, string proxyUserName, string proxyPassword) : this(serverUrl, userName, password, clientName)
        {
            Proxy = new WebProxy(proxyServer, proxyPort)
            {
                Credentials = new NetworkCredential(proxyUserName, proxyPassword)
            };
        }

        public Uri GetUrl()
        {
            return Url;
        }

        public void SetUrl(string url)
        {
            Url = new Uri(url);
        }

        public void SetUrl(Uri url)
        {
            Url = url;
        }

        public string GetUserName()
        {
            return UserName;
        }

        public void SetUserName(string username)
        {
            UserName = username;
        }

        public string GetPassword()
        {
            return Password;
        }

        public void SetPassword(string password)
        {
            Password = password;
        }

        public string GetClientName()
        {
            return ClientName;
        }

        public void SetClientName(string clientName)
        {
            ClientName = clientName;
        }

        public Version GetApiVersion()
        {
            return ApiVersion;
        }

        public void SetApiVersion(Version apiVersion)
        {
            ApiVersion = apiVersion;
        }

        public IWebProxy GetProxy()
        {
            return Proxy;
        }

        public void SetProxy(string host, int port)
        {
            Proxy = new WebProxy(host, port);
        }
    }
}

