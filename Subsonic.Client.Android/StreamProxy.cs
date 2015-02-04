using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Subsonic.Client.Items;

namespace Subsonic.Client.Android
{
    public class StreamProxy : IDisposable
    {
        Thread _thread;
        bool _isRunning;
        TcpListener _socket;
        int _port;
        TrackItem _trackItem;
        StreamToMediaPlayerTask _task;
        static readonly Lazy<StreamProxy> StreamProxyInstance = new Lazy<StreamProxy>(() => new StreamProxy());

        StreamProxy() { }

        public static StreamProxy Instance
        {
            get { return StreamProxyInstance.Value; }
        }

        public void SetTrackItem(TrackItem trackItem)
        {
            _trackItem = trackItem;
        }

        public int GetPort()
        {
            return _port;
        }

        public void Start()
        {
            // Create listening socket
            try
            {
                _socket = new TcpListener(IPAddress.Loopback, 0);
                _socket.Start();
                _port = ((IPEndPoint)_socket.LocalEndpoint).Port;

                _thread = new Thread(Run);
                _thread.Start();
            }
            catch
            {
            }
        }

        public void Stop()
        {
            _isRunning = false;

            if (_thread == null) return;

            _socket.Stop();
            _thread.Abort();
        }

        void Run()
        {
            _isRunning = true;

            _task = new StreamToMediaPlayerTask();

            SpinWait.SpinUntil(CheckForWork);
        }

        bool CheckForWork()
        {
            try
            {
                var client = _socket.AcceptTcpClient();

                if (!client.Connected)
                    return !_isRunning;

                _task.SetClient(client);

                if (_task.ProcessRequest())
                    _task.Run();
                else
                    _task.Dispose();
            }
            catch
            {
            }

            return !_isRunning;
        }

        public void Dispose()
        {
            if (_isRunning)
                Stop();

            if (_task != null)
                _task.Dispose();
        }

        class StreamToMediaPlayerTask : IDisposable
        {
            string _localPath;
            TcpClient _client;
            int _cbSkip;
            NetworkStream _inputStream;
            StreamReader _streamReader;
            const string Headers = "HTTP/1.1 200 OK\r\nContent-Type: application/octet-stream\r\nConnection: close\r\n\r\n";

            internal void SetClient(TcpClient client)
            {
                if (_client != null)
                    _client.Close();

                _client = client;
                _client.NoDelay = true;
                _client.ReceiveBufferSize = 4096;
                _cbSkip = 0;
                _localPath = null;
            }

            string ReadRequest()
            {
                String firstLine;

                try
                {
                    _inputStream = _client.GetStream();
                    _streamReader = new StreamReader(_inputStream);
                    firstLine = _streamReader.ReadLine();
                }
                catch (Exception)
                {
                    return null;
                }

                if (string.IsNullOrWhiteSpace(firstLine))
                    return null;

                var st = firstLine.Split(' ');
                return st.ElementAt(1).Substring(1);
            }

            internal bool ProcessRequest()
            {
                string request = ReadRequest();

                if (string.IsNullOrWhiteSpace(request))
                    return false;

                _localPath = Uri.UnescapeDataString(request);

                const int timeToWaitForFile = 5000;

                if (_localPath == null)
                    return false;

                var file = new FileInfo(_localPath);

                if (file.Exists)
                    return true;

                bool waitForFile = true;
                var sw = new Stopwatch();
                sw.Start();

                while (waitForFile)
                {
                    file.Refresh();

                    if (file.Exists)
                        return true;

                    Thread.Sleep(2);

                    waitForFile &= sw.ElapsedMilliseconds <= timeToWaitForFile;
                }

                sw.Stop();

                return false;
            }

            internal void Run()
            {
                var headerBytes = Encoding.ASCII.GetBytes(Headers);
                var buff = new byte[4096];

                try
                {
                    using (var output = _client.GetStream())
                    {
                        output.Write(headerBytes, 0, headerBytes.Length);

                        // See if there's more to send
                        var file = new FileInfo(_localPath);

                        // Loop as long as there's stuff to send
                        while (Instance._isRunning && _client.Connected)
                        {
                            file.Refresh();

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

                                        if (cbRead == 0)
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
                            if (Instance._trackItem.Cached)
                            {
                                // Make sure we have the latest file information
                                file.Refresh();

                                if (_cbSkip >= file.Length)
                                    break;
                            }

                            // If we did nothing this batch, block for a second
                            if (cbSentThisBatch == 0)
                                Thread.Sleep(100);
                        }
                    }
                }
                catch
                {
                }
                finally
                {
                    Dispose();
                    _client.Close();
                }
            }

            public void Dispose()
            {
                if (_streamReader != null)
                {
                    _streamReader.Close();
                    _streamReader.Dispose();
                    _streamReader = null;
                }

                if (_inputStream != null)
                {
                    _inputStream.Close();
                    _inputStream.Dispose();
                    _inputStream = null;
                }
            }
        }
    }
}
