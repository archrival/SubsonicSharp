using System;
using System.Globalization;
using System.IO;
using Subsonic.Client.Enums;
using Subsonic.Client.Interfaces;

namespace Subsonic.Client
{
    public sealed class FileLogger : IFileLogger, IDisposable
    {
        private readonly FileStream _logFile;
        private readonly TextWriter _logWriter;
        private readonly LoggingLevel _loggingLevel;

        public FileLogger(string path, LoggingLevel loggingLevel)
        {
            _loggingLevel = loggingLevel;
            _logFile = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            _logWriter = new StreamWriter(_logFile, System.Text.Encoding.UTF8);
        }

        public void Log(string entry, LoggingLevel loggingLevel)
        {
            if (_loggingLevel >= loggingLevel)
                _logWriter.WriteLine(FormatLogEntry(entry));
        }

        private static string FormatLogEntry(string entry)
        {
            return string.Format("{0} {1}", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss.fffffff", CultureInfo.InvariantCulture), entry);
        }

        public void Close()
        {
            _logWriter.Flush();
            _logFile.Flush();
            _logWriter.Close();
            _logFile.Close();
        }

        public void Dispose()
        {
            Close();

            _logWriter.Dispose();
            _logFile.Dispose();
        }
    }
}
