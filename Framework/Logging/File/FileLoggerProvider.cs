using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace Tlc.Logging.File
{
  public class FileLoggerProvider : ILoggerProvider
  {

    #region protected fields
    
    protected string path;
    protected readonly FileLoggerConfiguration configuration;
    protected readonly ConcurrentDictionary<string, FileLogger> loggers = new ConcurrentDictionary<string, FileLogger>();
    
    #endregion

    public FileLoggerProvider(string path, FileLoggerConfiguration configuration)
    {
      this.path = path;
      this.configuration = configuration;
    }

    public void Dispose()
    {
      loggers.Clear();
    }

    public ILogger CreateLogger(string categoryName)
    {
      return loggers.GetOrAdd(categoryName, name => new FileLogger(name, path, configuration));
    }
  }
}