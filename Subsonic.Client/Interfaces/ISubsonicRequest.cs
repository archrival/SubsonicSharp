using Subsonic.Common.Classes;
using Subsonic.Common.Enums;
using Subsonic.Common.Interfaces;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Subsonic.Client.Interfaces
{
    /// <summary>
    /// Defines methods to make a request to a Subsonic server.
    /// </summary>
    /// <typeparam name="T">Specifies the platform specific image format to be utilized.</typeparam>
    public interface ISubsonicRequest<T> where T : class, IDisposable
    {
        /// <summary>
        /// Get a Response for the specified Subsonic method.
        /// </summary>
        /// <param name="method" cref="Methods">Subsonic API method to call.</param>
        /// <param name="methodApiVersion" cref="Version">Subsonic API version of the method.</param>
        /// <param name="parameters" cref="SubsonicParameters">Parameters used by the method.</param>
        /// <param name="cancelToken" cref="CancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns cref="Response">Subsonic response</returns>
        Task<Response> RequestAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null);

        /// <summary>
        /// Save response to disk for the specified Subsonic method.
        /// </summary>
        /// <param name="path" cref="string">Directory to save the response to, the filename is provided by the server using the Content-Disposition header.</param>
        /// <param name="pathOverride" cref="bool">If specified, the value of path becomes the complete path including filename.</param>
        /// <param name="method" cref="Methods">Subsonic API method to call.</param>
        /// <param name="methodApiVersion" cref="Version">Subsonic API version of the method.</param>
        /// <param name="parameters" cerf="SubsonicParameters">Parameters used by the method.</param>
        /// <param name="cancelToken" cref="CancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns cref="long">Bytes transferred</returns>
        Task<long> RequestAsync(string path, bool pathOverride, Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null);

        /// <summary>
        /// Make a web request for the specified Subsonic method and do not wait for a response.
        /// </summary>
        /// <param name="method" cref="Methods">Subsonic API method to call.</param>
        /// <param name="methodApiVersion" cref="Version">Subsonic API version of the method.</param>
        /// <param name="parameters" cref="SubsonicParameters">Parameters used by the method.</param>
        /// <param name="cancelToken" cref="CancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns cref="Task">Task</returns>
        Task RequestWithoutResponseAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null);

        /// <summary>
        /// Get a string for the specified Subsonic method.
        /// </summary>
        /// <param name="method" cref="Methods">Subsonic API method to call.</param>
        /// <param name="methodApiVersion" cref="Version">Subsonic API version of the method.</param>
        /// <param name="parameters" cref="SubsonicParameters">Parameters used by the method.</param>
        /// <param name="cancelToken" cref="CancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns cref="string">String</returns>
        Task<string> StringRequestAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null);

        /// <summary>
        /// Get an Image for the specified Subsonic method.
        /// </summary>
        /// <typeparam name="T">Specifies the platform specific image format to be utilized.</typeparam>
        /// <param name="method" cref="Methods">Subsonic API method to call.</param>
        /// <param name="methodApiVersion" cref="Version">Subsonic API version of the method.</param>
        /// <param name="parameters" cref="SubsonicParameters">Parameters used by the method.</param>
        /// <param name="cancelToken" cref="CancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns cref="IImageFormat{T}">A platform specific image</returns>
        Task<IImageFormat<T>> ImageRequestAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null);

        /// <summary>
        /// Get content length in bytes for the specified Subsonic method.
        /// </summary>
        /// <param name="method" cref="Methods">Subsonic API method to call.</param>
        /// <param name="methodApiVersion" cref="Version">Subsonic API version of the method.</param>
        /// <param name="parameters" cref="SubsonicParameters">Parameters used by the method.</param>
        /// <param name="cancelToken" cref="CancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns cref="long">Content length</returns>
        Task<long> ContentLengthRequestAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null);

        /// <summary>
        /// Execute a setting change request.
        /// </summary>
        /// <param name="method" cref="SettingMethods">Subsonic settings method to call.</param>
        /// <param name="cancelToken" cref="CancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns cref="bool">true if a successful HTTP response was returned; otherwise, false.</returns>
        Task<bool> SettingChangeRequestAsync(SettingMethods method, CancellationToken? cancelToken = null);

        /// <summary>
        /// Get the default HttpClientHandler.
        /// </summary>
        /// <returns cref="HttpClientHandler">An HttpClientHandler</returns>
        HttpClientHandler GetClientHandler();

        /// <summary>
        /// Get the default HttpClient
        /// </summary>
        /// <param name="handler" cref="HttpMessageHandler">Handler containing common HttpClient settings (Proxy, Credentials, etc.).</param>
        /// <param name="addAuthentication">Add basic authentication header to every request.</param>
        /// <returns cref="HttpClient">An HttpClient</returns>
        HttpClient GetClient(HttpMessageHandler handler, bool addAuthentication = true);
    }
}