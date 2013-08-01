using Subsonic.Common.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Subsonic.Client.Common
{
    public class SubsonicRequest
    {
        public SubsonicClient Client { get; set; }

        public SubsonicRequest(SubsonicClient client)
        {
            Client = client;
        }

        /// <summary>
        /// Builds a URI to be used for the request.
        /// </summary>
        /// <param name="method">Subsonic API method to call.</param>
        /// <param name="methodApiVersion">Subsonic API version of the method.</param>
        /// <param name="parameters">Parameters used by the method.</param>
        /// <returns>string</returns>
        public string BuildRequestUri(Methods method, Version methodApiVersion, SubsonicParameters parameters = null)
        {
            string request = string.Format(CultureInfo.InvariantCulture, "{0}/rest/{1}.view?v={2}&c={3}", Client.ServerUrl, Enum.GetName(typeof(Methods), method), methodApiVersion, Client.Name);

            if (parameters != null && parameters.Parameters.Count > 0)
            {
                foreach (var parameter in parameters.Parameters)
                {
                    string key = string.Empty;
                    string value = string.Empty;

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

                    request += string.Format(CultureInfo.InvariantCulture, "&{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value));
                }
            }

            return request;
        }

        /// <summary>
        /// Builds a URI to be used for the request.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="method">Subsonic API method to call.</param>
        /// <param name="methodApiVersion">Subsonic API version of the method.</param>
        /// <param name="parameters">Parameters used by the method.</param>
        /// <returns>string</returns>
        public static string BuildRequestUriUser(SubsonicClient client, Methods method, Version methodApiVersion, SubsonicParameters parameters = null)
        {
            string encodedPassword = string.Format(CultureInfo.InvariantCulture, "enc:{0}", client.Password.ToHex());
            string request = string.Format(CultureInfo.InvariantCulture, "{0}/rest/{1}.view?v={2}&c={3}&u={4}&p={5}", client.ServerUrl, Enum.GetName(typeof(Methods), method), methodApiVersion, client.Name, client.UserName, encodedPassword);

            if (parameters != null && parameters.Parameters.Count > 0)
                request = parameters.Parameters.Cast<DictionaryEntry>().Aggregate(request, (current, parameter) => current + string.Format(CultureInfo.InvariantCulture, "&{0}={1}", HttpUtility.UrlEncode(parameter.Key.ToString()), HttpUtility.UrlEncode(parameter.Value.ToString())));

            return request;
        }
    }
}
