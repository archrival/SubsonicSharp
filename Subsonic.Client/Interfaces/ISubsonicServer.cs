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
        /// Gets or sets the base URL to use for communication with this server.
        /// </summary>
        /// <returns cref="Uri">The base URL of the Subsonic server.</returns>
        Uri Url { get; set; }

        /// <summary>
        /// Sets the base URL to use for communication with this server.
        /// </summary>
        /// <param name="url" cref="string">The base URL of the Subsonic server.</param>
        void SetUrl(string url);

        /// <summary>
        /// Gets or sets the user name to use for communication with this server.
        /// </summary>
        /// <returns cref="string">The user name.</returns>
        string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password to use for communication with this server.
        /// </summary>
        /// <returns cref="string">The password.</returns>
        string Password { get; set; }

        /// <summary>
        /// Gets or sets the client name to use for communication with this server.
        /// </summary>
        /// <returns cref="string">The client name.</returns>
        string ClientName { get; set; }

        /// <summary>
        /// Gets or sets the version of the Subsonic API for this server.
        /// </summary>
        /// <returns cref="Version">The Subsonic server API version.</returns>
        Version ApiVersion { get; set; }

        /// <summary>
        /// Gets or sets the IWebProxy for the instance.
        /// </summary>
        /// <returns cref="IWebProxy">IWebProxy specified for this server.</returns>
        IWebProxy Proxy { get; set; }

        /// <summary>
        /// Sets the IWebProxy for the instance using the specified host and port.
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

