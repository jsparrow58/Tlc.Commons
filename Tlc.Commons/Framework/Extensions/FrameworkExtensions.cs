using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tlc.Framework.Construction;

namespace Tlc.Framework.Extensions
{
    public static class FrameworkExtensions
    {
        #region Configuration


        /// <summary>
        /// 加载默认的配置文件
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
        /// 提供配置文件给构造器
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
