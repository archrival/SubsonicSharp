using Subsonic.Client.Common.Items;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Web;

namespace Subsonic.Client.Common
{
    public class StreamProxy
    {
        private Thread _thread;
        private bool _isRunning;
        private readonly TcpListener _socket;
        private readonly int _port;
        private readonly TrackItem _trackItem;
        private string _currentTrack;

        public StreamProxy(TrackItem trackItem)
        {
            // Create listening socket
            try
            {
                _socket = new TcpListener(IPAddress.Loopback, 0);
                _socket.Start();
                _port = ((IPEndPoint)_socket.LocalEndpoint).Port;
                _trackItem = trackItem;
            }
            catch
            {
            }
        }

        public int GetPort()
        {
            return _port;
        }

        public void Start()
        {
            _thread = new Thread(Run);
            _thread.Start();
        }

        public void Stop()
        {
            _currentTrack = null;
            _isRunning = false;
            _thread.Abort();
        }

        private void Run()
        {
            _isRunning = true;

            while (_isRunning)
            {
                try
                {
                    if (!_socket.Pending())
                        continue;

                    var client = _socket.AcceptTcpClient();

                    if (!client.Connected)
                        continue;

                    StreamToMediaPlayerTask task = new StreamToMediaPlayerTask(this, client);

                    if (task.ProcessRequest())
                         task.Run();
                }
                catch
                {
                }

                Thread.Sleep(10);
            }
        }

        private class StreamToMediaPlayerTask
        {
            private string _localPath;
            private readonly TcpClient _client;
            private int _cbSkip;
            private readonly StreamProxy _instance;

            public StreamToMediaPlayerTask(StreamProxy streamProxy, TcpClient client)
            {
                _client = client;
                _instance = streamProxy;
            }

            private string ReadRequest()
            {
                String firstLine;

                try
                {
                    var inputStream = _client.GetStream();
                    var streamReader = new StreamReader(inputStream);
                    firstLine = streamReader.ReadLine();
                }
                catch (Exception e)
                {
                    return null;
                }

                if (string.IsNullOrWhiteSpace(firstLine))
                    return null;

                var st = firstLine.Split(' ');
                return st.ElementAt(1).Substring(1);
            }

            public bool ProcessRequest()
            {
                string request = ReadRequest();

                if (string.IsNullOrWhiteSpace(request))
                    return false;

                _localPath = HttpUtility.UrlDecode(request, Encoding.UTF8);

                if (_localPath == _instance._currentTrack)
                    return false;

                return _localPath != null && new FileInfo(_localPath).Exists;
            }

            public void Run()
            {
                // Create HTTP header
                string headers = "HTTP/1.1 200 OK\r\n";
                headers += "Content-Type: application/octet-stream\r\n";
                headers += "Connection: close\r\n";
                headers += "\r\n";

                var headerBytes = Encoding.ASCII.GetBytes(headers);
                var buff = new byte[64 * 1024];

                try
                {
                    using (var output = _client.GetStream())
                    {
                        output.Write(headerBytes, 0, headerBytes.Length);

                        // Loop as long as there's stuff to send
                        while (_instance._isRunning && _client.Connected)
                        {
                            // See if there's more to send
                            var file = new FileInfo(_localPath);
                            int cbSentThisBatch = 0;

                            if (file.Exists)
                            {
                                using (FileStream input = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                                {
                                    input.Seek(_cbSkip, SeekOrigin.Current);
                                    long cbToSendThisBatch = input.Length;

                                    while (cbToSendThisBatch > 0)
                                    {
                                        int cbToRead = (int)Math.Min(cbToSendThisBatch, buff.Length);
                                        int cbRead = input.Read(buff, 0, cbToRead);

                                        if (cbRead == -1)
                                            break;

                                        cbToSendThisBatch -= cbRead;

                                        if (output.CanWrite)
                                            output.Write(buff, 0, cbRead);

                                        _cbSkip += cbRead;
                                        cbSentThisBatch += cbRead;
                                    }
                                }
                            }

                            // Done regardless of whether or not it thinks it is
                            if (_instance._trackItem.Cached && _cbSkip >= file.Length)
                                break;

                            // If we did nothing this batch, block for a second
                            if (cbSentThisBatch == 0)
                                Thread.Sleep(1000);
                        }
                    }
                }
                catch
                {
                    
                }
                finally
                {
                    _instance._currentTrack = null;
                    _client.Close();
                }
            }
        }
    }
}