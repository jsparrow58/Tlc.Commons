using Microsoft.Extensions.DependencyInjection;
using SJ.Framework.Construction;
using SJ.Logging.Extensions;
using System;
using static SJ.Framework.FrameworkDI;

namespace SJ.Framework
{
    public static class Framework
    {

        #region 公共属性

        public static FrameworkConstruction Construction { get; private set; }

        public static IServiceProvider Provider => Construction?.Provider;

        #endregion

        #region Extension Methods

        public static void Build(this FrameworkConstruction construction, bool logStarted = true)
        {
            // Build the service provider
            construction.Build();

            if (logStarted)
                Logger.LogCriticalSource($"SJ Framework started in {FrameworkEnvironment.Configuration}...");
        }

        public static void Build(IServiceProvider provider, bool logStarted = true)
        {
            Construction.Build(provider);

            if (logStarted)
                Logger.LogCriticalSource($"SJ Framework started in {FrameworkEnvironment.Configuration}...");
        }

        public static FrameworkConstruction Construct<T>()
          where T : FrameworkConstruction, new()
        {
            Construction = new T();
            return Construction;
        }

        public static FrameworkConstruction Construct<T>(T constructionInstance)
          where T : FrameworkConstruction
        {
            Construction = constructionInstance;
            return Construction;
        }

        public static T Service<T>()
        {
            return Provider.GetService<T>();
        }

        #endregion

    }
}