using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SJ.Environment;
using SJ.ExceptionHandling;

namespace SJ.Framework
{
  public static class FrameworkDI
  {
    public static IConfiguration Configuration => Framework.Provider?.GetService<IConfiguration>();

    public static ILogger Logger => Framework.Provider?.GetService<ILogger>();

    public static ILoggerFactory LoggerFactory => Framework.Provider?.GetService<ILoggerFactory>();

    public static IFrameworkEnvironment FrameworkEnvironment => Framework.Provider?.GetService<IFrameworkEnvironment>();

    /// <summary>
    ///   获取异常处理器
    /// </summary>
    public static IExceptionHandler ExceptionHandler => Framework.Provider?.GetService<IExceptionHandler>();
  }
}