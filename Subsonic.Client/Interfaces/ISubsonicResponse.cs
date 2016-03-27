using System;
using System.Threading;
using System.Threading.Tasks;
using Subsonic.Common.Enums;
using Subsonic.Common.Interfaces;

namespace Subsonic.Client.Interfaces
{
    /// <summary>
    /// Defines methods to return a response from a Subsonic server.
    /// </summary>
    /// <typeparam name="T">Specifies the platform specific image format to be utilized.</typeparam>
    public interface ISubsonicResponse<T> where T : class, IDisposable
    {        
        /// <summary>
        /// Get a boolean response from the Subsonic server for the specified Subsonic method.
        /// </summary>
        /// <param name="method" cref="Methods">Subsonic API method to call.</param>
        /// <param name="methodApiVersion" cref="Version">Subsonic API version of the method.</param>
        /// <param name="parameters" cref="SubsonicParameters">Parameters used by the method.</param>
        /// <param name="cancelToken" cref="CancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns cref="bool">true on success; otherwise, false</returns>
        Task<bool> GetResponseAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null);

        /// <summary>
        /// Get a response from the Subsonic server for the specified Subsonic method.
        /// </summary>
        /// <typeparam name="TResponse">Object type the method will return.</typeparam>
        /// <param name="method" cref="Methods">Subsonic API method to call.</param>
        /// <param name="methodApiVersion" cref="Version">Subsonic API version of the method.</param>
        /// <param name="parameters" cref="SubsonicParameters">Parameters used by the method.</param>
        /// <param name="cancelToken" cref="CancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns>Response</returns>
        Task<TResponse> GetResponseAsync<TResponse>(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null);

        /// <summary>
        /// Get a response as a string from the Subsonic server for the specified Subsonic method.
        /// </summary>
        /// <param name="method" cref="Methods">Subsonic API method to call.</param>
        /// <param name="methodApiVersion" cref="Version">Subsonic API version of the method.</param>
        /// <param name="parameters" cref="SubsonicParameters">Parameters used by the method.</param>
        /// <param name="cancelToken" cref="CancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns cref="string">String</returns>
        Task<string> GetStringResponseAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null);

        /// <summary>
        /// Get a response and save it to a path from the Subsonic server for the specified Subsonic method.
        /// </summary>
        /// <param name="path" cref="string">Where to save the response.</param>
        /// <param name="pathOverride" cref="bool">Override filename specified in the HTTP headers.</param>
        /// <param name="method" cref="Methods">Subsonic API method to call.</param>
        /// <param name="methodApiVersion" cref="Version">Subsonic API version of the method.</param>
        /// <param name="parameters" cref="SubsonicParameters">Parameters used by the method.</param>
        /// <param name="cancelToken" cref="CancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns cref="long">Returns bytes transferred</returns>
        Task<long> GetResponseAsync(string path, bool pathOverride, Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null);

        /// <summary>
        /// Get a boolean response from the Subsonic server for the specified Subsonic method.
        /// </summary>
        /// <param name="method" cref="Methods">Subsonic API method to call.</param>
        /// <param name="methodApiVersion" cref="Version">Subsonic API version of the method.</param>
        /// <param name="parameters" cref="SubsonicParameters">Parameters used by the method.</param>
        /// <param name="cancelToken" cref="CancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns cref="Task">Task</returns>
        Task GetNoResponseAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null);

        /// <summary>
        /// Get a content length response from the Subsonic server for the specified Subsonic method.
        /// </summary>
        /// <param name="method" cref="Methods">Subsonic API method to call.</param>
        /// <param name="methodApiVersion" cref="Version">Subsonic API version of the method.</param>
        /// <param name="parameters" cref="SubsonicParameters">Parameters used by the method.</param>
        /// <param name="cancelToken" cref="CancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns cref="long">Content length</returns>
        Task<long> GetContentLengthAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null);

        /// <summary>
        /// Get a platform specific image response from the Subsonic server for the specified Subsonic method.
        /// </summary>
        /// <param name="method" cref="Methods">Subsonic API method to call.</param>
        /// <param name="methodApiVersion" cref="Version">Subsonic API version of the method.</param>
        /// <param name="parameters" cref="SubsonicParameters">Parameters used by the method.</param>
        /// <param name="cancelToken" cref="CancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns cref="IImageFormat{T}">A platform spcecific image</returns>
        Task<IImageFormat<T>> GetImageResponseAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null);

        /// <summary>
        /// Get a response from the Subsonic server for the specified Subsonic setting method.
        /// </summary>
        /// <param name="method" cref="SettingMethods">Subsonic setting method to call.</param>
        /// <param name="cancelToken" cref="CancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns cref="bool">true if a successful HTTP response was returned; otherwise, false.</returns>
        Task<bool> GetSettingChangeResponseAsync(SettingMethods method, CancellationToken? cancelToken = null);
    }
}
