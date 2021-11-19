using Microsoft.Extensions.Logging;

namespace SJ.Logging.File
{
    public class FileLoggerConfiguration
    {

        #region Public Properties

        public LogLevel LogLevel { get; set; } = LogLevel.Trace;

        public bool LogTime { get; set; } = true;

        public bool LogAtTop { get; set; } = true;

        public bool OutputLogLeave { get; set; } = true;

        #endregion

    }
}