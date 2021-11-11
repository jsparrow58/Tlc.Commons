using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tlc.Framework.Construction;

namespace Tlc.Logging.File
{
  public static class FileLoggerExtensions
  {
    public static ILoggingBuilder AddFile(this ILoggingBuilder builder, string path,
      FileLoggerConfiguration configuration = null)
    {
      if (configuration == null) configuration = new FileLoggerConfiguration();
      builder.AddProvider(new FileLoggerProvider(path, configuration));
      return builder;
    }

    public static FrameworkConstruction AddFileLogger(this FrameworkConstruction construction, string path = "log.txt",
      bool logTop = true)
    {
      construction.Services.AddLogging(options =>
      {
        options.AddFile(path, new FileLoggerConfiguration() { LogAtTop = logTop });
      });

      return construction;
    }
  }

}