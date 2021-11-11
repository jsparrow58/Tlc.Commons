using System;
using System.Collections.Concurrent;
using System.IO;
using Microsoft.Extensions.Logging;

namespace SJ.Logging.File
{
  public class FileLogger : ILogger
  {

    #region Constructor

    public FileLogger(string categoryName, string filePath, FileLoggerConfiguration configuration)
    {
      this.categoryName = categoryName;
      this.filePath = Path.GetFullPath(filePath);
      directory = Path.GetDirectoryName(this.filePath);
      this.configuration = configuration;
    }

    #endregion

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
      Func<TState, Exception, string> formatter)
    {
      if (!IsEnabled(logLevel)) return;

      var currentTime = DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss");

      var logLeveString = configuration.OutputLogLeave ? $"{logLevel.ToString().ToUpper()}: " : "";
      var logTimeString = configuration.LogTime ? $"[{currentTime}] " : "";
      // Get the formatted message string
      var message = formatter(state, exception);
      var output = $"{logLeveString}{logTimeString}{message}{System.Environment.NewLine}";

      var normalizedPath = filePath.ToUpper();

      var fileLock = default(object);

      lock (fileLockLock)
      {
        fileLock = fileLocks.GetOrAdd(normalizedPath, path => new object());
      }

      lock (fileLock)
      {
        if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

        using (var fileStream = new StreamWriter(System.IO.File.Open(filePath, FileMode.OpenOrCreate,
          FileAccess.ReadWrite, FileShare.ReadWrite)))
        {
          // 到文件的末尾
          fileStream.BaseStream.Seek(0, SeekOrigin.End);
          // Notes: 忽略配置中的logToTop，因为对操作系统上的文件无效。
          // 把消息写入文件
          fileStream.Write(output);
        }
      }
    }

    public bool IsEnabled(LogLevel logLevel)
    {
      return logLevel >= configuration.LogLevel;
    }

    public IDisposable BeginScope<TState>(TState state)
    {
      return null;
    }

    #region static properties

    /// <summary>
    ///   日志文件并行锁
    /// </summary>
    protected static ConcurrentDictionary<string, object> fileLocks = new ConcurrentDictionary<string, object>();

    /// <summary>
    ///   文件并行锁的锁
    /// </summary>
    protected static object fileLockLock = new object();

    #endregion

    #region Protected properties

    protected string categoryName;

    protected string filePath;

    protected string directory;

    protected FileLoggerConfiguration configuration;

    #endregion

  }
}