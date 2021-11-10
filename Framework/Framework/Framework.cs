using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tlc.Framework.Construction;
using static Tlc.Framework.FrameworkDI;

namespace Tlc.Framework
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

            // TODO: implement Logger
            /*if(logStarted)
                Logger.LogCritical;*/
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

        private static T Service<T>()
        {
            return Provider.GetService<T>();
        }

        #endregion
    }
}
