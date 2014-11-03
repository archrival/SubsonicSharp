using Subsonic.Client.Enums;

namespace Subsonic.Client.Interfaces
{
    public interface IFileLogger
    {
        void Log(string entry, LoggingLevel loggingLevel);
        void Close();
        void Dispose();
    }
}
