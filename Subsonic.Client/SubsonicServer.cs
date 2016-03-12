using Subsonic.Client.Extensions;
using Subsonic.Client.Interfaces;
using Subsonic.Common.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

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
            return ApiVersion >= Common.SubsonicApiVersions.Version1_13_0;
        }

        public Uri BuildRequestUri(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, bool checkForTokenUsability = true)
        {
            UriBuilder uriBuilder = new UriBuilder(Url);

            StringBuilder pathBuilder = new StringBuilder(uriBuilder.Path);
            pathBuilder.AppendFormat("/rest/{0}.view", method.GetXmlEnumAttribute());
            uriBuilder.Path = Regex.Replace(pathBuilder.ToString(), "/+", "/");

            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.AppendFormat("v={0}&c={1}", methodApiVersion, ClientName);

            if (parameters != null && parameters.Parameters.Count > 0)
            {
                foreach (object parameter in parameters.Parameters)
                {
                    string key = string.Empty;
                    string value = string.Empty;

                    if (parameter is DictionaryEntry)
                    {
                        DictionaryEntry entry = (DictionaryEntry)parameter;

                        key = entry.Key.ToString();
                        value = entry.Value.ToString();
                    }

                    if (parameter is KeyValuePair<string, string>)
                    {
                        KeyValuePair<string, string> entry = (KeyValuePair<string, string>)parameter;

                        key = entry.Key;
                        value = entry.Value;
                    }

                    queryBuilder.AppendFormat("&{0}={1}", Uri.EscapeDataString(key), Uri.EscapeDataString(value));
                }
            }

            if (checkForTokenUsability && ShouldUseNewAuthentication())
            {
                SubsonicToken subsonicToken = SubsonicAuthentication.GetToken();
                queryBuilder.AppendFormat("&u={0}&t={1}&s={2}", UserName, subsonicToken.Token, subsonicToken.Salt);
            }

            uriBuilder.Query = queryBuilder.ToString();
            return uriBuilder.Uri;
        }

        public Uri BuildRequestUriUser(Methods method, Version methodApiVersion, SubsonicParameters parameters = null)
        {
            UriBuilder uriBuilder = new UriBuilder(BuildRequestUri(method, methodApiVersion, parameters, false));

            StringBuilder queryBuilder = new StringBuilder(uriBuilder.Query.TrimStart('?'));

            if (ShouldUseNewAuthentication())
            {
                SubsonicToken subsonicToken = SubsonicAuthentication.GetToken();
                queryBuilder.AppendFormat("&u={0}&t={1}&s={2}", UserName, subsonicToken.Token, subsonicToken.Salt);
            }
            else
            {
                string encodedPassword = string.Format("enc:{0}", Password.ToHexString());
                queryBuilder.AppendFormat("&u={0}&p={1}", UserName, encodedPassword);
            }

            uriBuilder.Query = queryBuilder.ToString();

            return uriBuilder.Uri;
        }

        public Uri BuildSettingsRequestUri(SettingMethods method)
        {
            UriBuilder uriBuilder = new UriBuilder(Url)
            {
                UserName = UserName,
                Password = Password
            };

            StringBuilder pathBuilder = new StringBuilder(uriBuilder.Path);
            pathBuilder.Append("/musicFolderSettings.view");
            uriBuilder.Path = Regex.Replace(pathBuilder.ToString(), "/+", "/");

            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.AppendFormat("{0}", method.GetXmlEnumAttribute());

            uriBuilder.Query = queryBuilder.ToString();

            return uriBuilder.Uri;
        }
    }
}