using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tlc.Framework.Construction;

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

            /*if(logStarted)
                Logger*/
        }

        #endregion
    }
}
