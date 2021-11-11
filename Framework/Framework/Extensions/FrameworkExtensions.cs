using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SJ.ExceptionHandling;
using SJ.Framework.Construction;

namespace SJ.Framework.Extensions
{
  public static class FrameworkExtensions
  {

    public static FrameworkConstruction AddDefaultService(this FrameworkConstruction construction)
    {
      construction.AddDefaultExceptionHandler()
        .AddDefaultLogger();

      return construction;
    }

    public static FrameworkConstruction AddDefaultLogger(this FrameworkConstruction construction)
    {
      construction.Services.AddLogging(options =>
      {
        // 设置debug为默认日志等级
        options.SetMinimumLevel(LogLevel.Debug);
        // 从配置中读取日志配置
        options.AddConfiguration(construction.Configuration.GetSection("Logging"));
        // add console logger
        options.AddConsole();
        // add debug logger
        options.AddDebug();
      });
      construction.Services.AddTransient(provider => provider.GetService<ILoggerFactory>().CreateLogger("tlc"));
      return construction;
    }

    /// <summary>
    ///   注入默认的异常处理程序到框架当中
    /// </summary>
    /// <param name="construction">框架构建器</param>
    /// <returns></returns>
    public static FrameworkConstruction AddDefaultExceptionHandler(this FrameworkConstruction construction)
    {
      construction.Services.AddSingleton<IExceptionHandler>(new DefaultExceptionHandler());
      return construction;
    }

    #region Configuration

    /// <summary>
    ///   加载默认的配置文件
    /// </summary>
    /// <param name="construction"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static FrameworkConstruction AddDefaultConfiguration(this FrameworkConstruction construction,
      Action<IConfigurationBuilder> configure = null)
    {
      // 构建自定义配置源
      var configurationBuilder = new ConfigurationBuilder()
        // 添加环境变量
        .AddEnvironmentVariables();

      if (!construction.Environment.IsMobile)
      {
        // 加载配置文件
        configurationBuilder.SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        configurationBuilder.AddJsonFile("appsettings.json", true, true);

        configurationBuilder.AddJsonFile($"appsettings.{construction.Environment.Configuration}.json", true,
          true);
      }

      // 应用用户个性配置
      configure?.Invoke(configurationBuilder);
      // 配置注入到services当中
      var configuration = configurationBuilder.Build();
      construction.Services.AddSingleton<IConfiguration>(configuration);
      // 应用构建器配置
      construction.UseConfiguration(configuration);

      // 支持链式操作
      return construction;
    }

    /// <summary>
    ///   提供配置文件给构造器
    /// </summary>
    /// <param name="construction">构造器</param>
    /// <param name="configuration">配置项</param>
    /// <returns></returns>
    public static FrameworkConstruction AddConfiguration(this FrameworkConstruction construction,
      IConfiguration configuration)
    {
      construction.UseConfiguration(configuration);
      construction.Services.AddSingleton(configuration);

      return construction;
    }

    #endregion

  }
}