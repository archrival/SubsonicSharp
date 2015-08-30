using System;
using System.Net;
using Subsonic.Common.Enums;

namespace Subsonic.Client.Interfaces
{
    /// <summary>
    /// Defines methods to store and retrieve information about a specific Subsonic server.
    /// </summary>
    public interface ISubsonicServer
    {
        /// <summary>
        /// Returns the base URL to use for communication with this server.
        /// </summary>
        /// <returns cref="Uri">The base URL of the Subsonic server.</returns>
        Uri GetUrl();

        /// <summary>
        /// Sets the base URL to use for communication with this server.
        /// </summary>
        /// <param name="url" cref="string">The base URL of the Subsonic server.</param>
        void SetUrl(string url);

        /// <summary>
        /// Sets the base URL to use for communication with this server.
        /// </summary>
        /// <param name="url" cref="Uri">The client name.</param>
        void SetUrl(Uri url);

        /// <summary>
        /// Returns the user name to use for communication with this server.
        /// </summary>
        /// <returns cref="string">The user name.</returns>
        string GetUserName();

        /// <summary>
        /// Sets the user name to use for communication with this server.
        /// </summary>
        /// <param name="username" cref="string">The user name.</param>
        void SetUserName(string username);

        /// <summary>
        /// Returns the password to use for communication with this server.
        /// </summary>
        /// <returns cref="string">The password.</returns>
        string GetPassword();

        /// <summary>
        /// Sets the password to use for communication with this server.
        /// </summary>
        /// <param name="password" cref="string">The password.</param>
        void SetPassword(string password);

        /// <summary>
        /// Returns the client name to use for communication with this server.
        /// </summary>
        /// <returns cref="string">The client name.</returns>
        string GetClientName();

        /// <summary>
        /// Sets the client name to use for communication with this server.
        /// </summary>
        /// <param name="clientName" cref="string">The client name.</param>
        void SetClientName(string clientName);

        /// <summary>
        /// Returns the version of the Subsonic API for this server.
        /// </summary>
        /// <returns cref="Version">The Subsonic server API version.</returns>
        Version GetApiVersion();

        /// <summary>
        /// Sets the version of the Subsonic API for this server.
        /// </summary>
        /// <param name="apiVersion" cref="Version">The Subsonic server API version.</param>
        void SetApiVersion(Version apiVersion);

        /// <summary>
        /// Returns the IWebProxy for the instance.
        /// </summary>
        /// <returns cref="IWebProxy">IWebProxy specified for this server.</returns>
        IWebProxy GetProxy();

        /// <summary>
        /// Sets the IWebProxy for the instance.
        /// </summary>
        /// <param name="host" cref="string">The proxy hostname.</param>
        /// <param name="port" cref="int">The proxy port.</param>
        void SetProxy(string host, int port);

        /// <summary>
        /// Builds a URI to be used for the request.
        /// </summary>
        /// <param name="method" cref="Methods">Subsonic API method to call.</param>
        /// <param name="methodApiVersion" cref="Version">Subsonic API version of the method.</param>
        /// <param name="parameters" cref="SubsonicParameters">Parameters used by the method.</param>
        /// <param name="checkForTokenUsability">Should check for usability of authentication token.</param>
        /// <returns cref="Uri">URL for the specified Subsonic method</returns>
        Uri BuildRequestUri(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, bool checkForTokenUsability = true);

        /// <summary>
        /// Builds a URI to be used for the request that explicitly provides the username and password as parameters.
        /// </summary>
        /// <param name="method" cref="Methods">Subsonic API method to call.</param>
        /// <param name="methodApiVersion" cref="Version">Subsonic API version of the method.</param>
        /// <param name="parameters" cref="SubsonicParameters">Parameters used by the method.</param>
        /// <returns cref="Uri">URL for the specified Subsonic method</returns>
        Uri BuildRequestUriUser(Methods method, Version methodApiVersion, SubsonicParameters parameters = null);

        /// <summary>
        /// Builds a URI to be used for the request.
        /// </summary>
        /// <param name="method" cref="SettingMethods">Subsonic API method to call.</param>
        /// <returns cref="Uri">URL for the specified Subsonic setting method</returns>
        Uri BuildSettingsRequestUri(SettingMethods method);
    }
}

