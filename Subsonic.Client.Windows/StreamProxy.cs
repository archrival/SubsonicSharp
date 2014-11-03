using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Subsonic.Client.Items;

namespace Subsonic.Client.Windows
{
    public sealed class StreamProxy : IDisposable
    {
        private Task _task;
        private bool _isRunning;
        private TcpListener _socket;
        private int _port;
        private TrackItem _trackItem;
        private StreamToMediaPlayerTask _mediaPlayerTask;
        private CancellationTokenSource _cancelTokenSource;
        private static readonly Lazy<StreamProxy> StreamProxyInstance = new Lazy<StreamProxy>(() => new StreamProxy());

        private StreamProxy() { }

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

                _cancelTokenSource = new CancellationTokenSource();
                _task = new Task(StartQueue, _cancelTokenSource.Token);
                _task.Start();
            }
            catch (Exception ex)
            {
            }
        }

        public void Stop()
        {
            _isRunning = false;

            if (_task == null) return;

            _cancelTokenSource.Cancel();
        }

        private void StartQueue()
        {
            _isRunning = true;

            _mediaPlayerTask = new StreamToMediaPlayerTask(this);

            SpinWait.SpinUntil(CheckForWork);
        }

        private bool CheckForWork()
        {
            try
            {
                if (!_socket.Pending())
                    return !_isRunning;

                var client = _socket.AcceptTcpClient();

                if (!client.Connected)
                    return !_isRunning;

                _mediaPlayerTask.SetClient(client);

                if (_mediaPlayerTask.ProcessRequest())
                    _mediaPlayerTask.StreamToClient();
                else
                    _mediaPlayerTask.Dispose();
            }
            catch (Exception ex)
            {
            }

            return !_isRunning;
        }

        public void Dispose()
        {
            if (_isRunning)
                Stop();

            if (_mediaPlayerTask != null)
                _mediaPlayerTask.Dispose();
        }

        private sealed class StreamToMediaPlayerTask : IDisposable
        {
            private string _localPath;
            private TcpClient _client;
            private int _cbSkip;
            private readonly StreamProxy _instance;
            private NetworkStream _inputStream;
            private StreamReader _streamReader;
            private const int TimeToWaitForFile = 10000;

            public StreamToMediaPlayerTask(StreamProxy streamProxy)
            {
                _instance = streamProxy;
            }

            internal void SetClient(TcpClient client)
            {
                if (_client != null)
                    _client.Close();

                _client = client;
                _client.NoDelay = true;
                _client.ReceiveBufferSize = 8096;
                _client.SendBufferSize = 8096;
                _client.SendTimeout = 100;
                _client.ReceiveTimeout = 100;
                _cbSkip = 0;
                _localPath = null;
            }

            private string ReadRequest()
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

                if (_localPath == null)
                    return false;

                FileInfo file = new FileInfo(_localPath);

                return file.Exists || SpinWait.SpinUntil(() => CheckFileToStreamExists(file), TimeToWaitForFile);
            }

            private static bool CheckFileToStreamExists(FileSystemInfo file)
            {
                file.Refresh();
                return file.Exists;
            }

            internal void StreamToClient()
            {
                var headerBytes = GetHeaderBytes();

                try
                {
                    using (var output = _client.GetStream())
                    {
                        output.Write(headerBytes, 0, headerBytes.Length);

                        // See if there's more to send
                        var file = new FileInfo(_localPath);
                        SpinWait.SpinUntil(() => SendData(output, file));
                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    Dispose();
                    _client.Close();
                }
            }

            private static byte[] GetHeaderBytes()
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("HTTP/1.1 200 OK");
                sb.AppendLine("Content-Type: application/octet-stream");
                sb.AppendLine("Connection: close");
                sb.AppendLine();
                
                return Encoding.ASCII.GetBytes(sb.ToString());
            }

            private bool SendData(NetworkStream output, FileInfo file)
            {
                if (!_instance._isRunning || !_client.Connected || !output.CanWrite)
                    return false;

                file.Refresh();

                using (FileStream input = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    var buff = new byte[8096];

                    input.Seek(_cbSkip, SeekOrigin.Current);
                    long cbToSendThisBatch = input.Length;

                    while (cbToSendThisBatch > 0)
                    {
                        int cbToRead = (int) Math.Min(cbToSendThisBatch, buff.Length);
                        int cbRead = input.Read(buff, 0, cbToRead);

                        if (cbRead == 0)
                            break;

                        cbToSendThisBatch -= cbRead;

                        output.Write(buff, 0, cbRead);

                        _cbSkip += cbRead;
                    }
                }

                file.Refresh();

                if (!_instance._trackItem.Cached)
                    return false;
                
                return _cbSkip >= file.Length;
            }

            public void Dispose()
            {
                if (_streamReader != null)
                {
                    _streamReader.Close();
                    _streamReader.Dispose();
                    _streamReader = null;
                }

                if (_inputStream == null)
                    return;

                _inputStream.Close();
                _inputStream.Dispose();
                _inputStream = null;
            }
        }
    }
}
