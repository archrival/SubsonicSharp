using System;
using System.Net;

namespace Subsonic.Client.Interfaces
{
    public interface ISubsonicServer
    {
        Uri GetUrl();
        void SetUrl(string url);
        void SetUrl(Uri url);
        string GetUserName();
        void SetUserName(string username);
        string GetPassword();
        void SetPassword(string password);
        string GetClientName();
        void SetClientName(string clientName);
        Version GetApiVersion();
        void SetApiVersion(Version apiVersion);
        IWebProxy GetProxy();
        void SetProxy(string host, int port);
    }
}

