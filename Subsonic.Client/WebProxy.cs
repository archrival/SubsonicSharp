using System;
using System.Globalization;
using System.Net;

namespace Subsonic.Client
{
    public class WebProxy : IWebProxy
    {
        public ICredentials Credentials { get; set; }

        private readonly Uri _proxyUri;

        public WebProxy(Uri proxyUri)
        {
            _proxyUri = proxyUri;
        }

        public WebProxy(string host, int port) : this(new Uri("http://" + host + ":" + port.ToString(CultureInfo.InvariantCulture)))
        {
        }

        public Uri GetProxy(Uri destination)
        {
            return _proxyUri;
        }

        public bool IsBypassed(Uri host)
        {
            return false;
        }
    }
}