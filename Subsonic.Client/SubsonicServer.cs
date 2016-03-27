using Subsonic.Client.Extensions;
using Subsonic.Client.Interfaces;
using Subsonic.Common.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Subsonic.Common;

namespace Subsonic.Client
{
    public class SubsonicServer : ISubsonicServer
    {
        public Uri Url { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ClientName { get; set; }
        public Version ApiVersion { get; set; }
        public IWebProxy Proxy { get; set; }
        private ISubsonicAuthentication SubsonicAuthentication { get; }

        public SubsonicServer(Uri serverUrl, string userName, string password, string clientName)
        {
            Url = serverUrl;
            UserName = userName;
            Password = password;
            ClientName = clientName;
            SubsonicAuthentication = new SubsonicAuthentication(Password);
            ApiVersion = SubsonicApiVersion.Max;
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

        public void SetUrl(string url)
        {
            Url = new Uri(url);
        }

        public void SetProxy(string host, int port)
        {
            Proxy = new WebProxy(host, port);
        }

        private bool ShouldUseNewAuthentication()
        {
            return ApiVersion >= SubsonicApiVersion.Version1_13_0;
        }

        public Uri BuildRequestUri(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, bool checkForTokenUsability = true)
        {
            var uriBuilder = new UriBuilder(Url);

            var pathBuilder = new StringBuilder(uriBuilder.Path);
            pathBuilder.AppendFormat("/rest/{0}.view", method.GetXmlEnumAttribute());
            uriBuilder.Path = Regex.Replace(pathBuilder.ToString(), "/+", "/");

            var queryBuilder = new StringBuilder();
            queryBuilder.AppendFormat("v={0}&c={1}", methodApiVersion, ClientName);

            if (parameters != null && parameters.Parameters.Count > 0)
            {
                foreach (object parameter in parameters.Parameters)
                {
                    var key = string.Empty;
                    var value = string.Empty;

                    if (parameter is DictionaryEntry)
                    {
                        var entry = (DictionaryEntry)parameter;

                        key = entry.Key.ToString();
                        value = entry.Value.ToString();
                    }

                    if (parameter is KeyValuePair<string, string>)
                    {
                        var entry = (KeyValuePair<string, string>)parameter;

                        key = entry.Key;
                        value = entry.Value;
                    }

                    queryBuilder.AppendFormat("&{0}={1}", Uri.EscapeDataString(key), Uri.EscapeDataString(value));
                }
            }

            if (checkForTokenUsability && ShouldUseNewAuthentication())
            {
                var subsonicToken = SubsonicAuthentication.GetToken();
                queryBuilder.AppendFormat("&u={0}&t={1}&s={2}", UserName, subsonicToken.Token, subsonicToken.Salt);
            }

            uriBuilder.Query = queryBuilder.ToString();
            return uriBuilder.Uri;
        }

        public Uri BuildRequestUriUser(Methods method, Version methodApiVersion, SubsonicParameters parameters = null)
        {
            var uriBuilder = new UriBuilder(BuildRequestUri(method, methodApiVersion, parameters, false));

            var queryBuilder = new StringBuilder(uriBuilder.Query.TrimStart('?'));

            if (ShouldUseNewAuthentication())
            {
                var subsonicToken = SubsonicAuthentication.GetToken();
                queryBuilder.AppendFormat("&u={0}&t={1}&s={2}", UserName, subsonicToken.Token, subsonicToken.Salt);
            }
            else
            {
                string encodedPassword = $"enc:{Password.ToHexString()}";
                queryBuilder.AppendFormat("&u={0}&p={1}", UserName, encodedPassword);
            }

            uriBuilder.Query = queryBuilder.ToString();

            return uriBuilder.Uri;
        }

        public Uri BuildSettingsRequestUri(SettingMethods method)
        {
            var uriBuilder = new UriBuilder(Url)
            {
                UserName = UserName,
                Password = Password
            };

            var pathBuilder = new StringBuilder(uriBuilder.Path);
            pathBuilder.Append("/musicFolderSettings.view");
            uriBuilder.Path = Regex.Replace(pathBuilder.ToString(), "/+", "/");

            var queryBuilder = new StringBuilder();
            queryBuilder.AppendFormat("{0}", method.GetXmlEnumAttribute());

            uriBuilder.Query = queryBuilder.ToString();

            return uriBuilder.Uri;
        }

        private const int HashSeed = 73; // Should be prime number
        private const int HashFactor = 17; // Should be prime number

        public override int GetHashCode()
        {
            var hash = HashSeed;
            hash = (hash * HashFactor) + typeof(SubsonicServer).GetHashCode();

            if (Url != null)
                hash = (hash * HashFactor) + Url.GetHashCode();

            if (UserName != null)
                hash = (hash * HashFactor) + UserName.GetHashCode();

            if (Password != null)
                hash = (hash * HashFactor) + Password.GetHashCode();

            if (ClientName != null)
                hash = (hash * HashFactor) + ClientName.GetHashCode();

            if (ApiVersion != null)
                hash = (hash * HashFactor) + ApiVersion.GetHashCode();

            return hash;
        }
    }
}