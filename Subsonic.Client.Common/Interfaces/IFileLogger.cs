using Subsonic.Client.Common.Enums;

namespace Subsonic.Client.Common.Interfaces
{
    public interface IFileLogger
    {
        void Log(string entry, LoggingLevel loggingLevel);

        void Close();

        void Dispose();
    }
}
