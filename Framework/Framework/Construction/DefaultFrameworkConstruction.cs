using Microsoft.Extensions.Configuration;
using System;

namespace SJ.Framework.Construction
{
    public class DefaultFrameworkConstruction : FrameworkConstruction
    {
        #region Constructor

        public DefaultFrameworkConstruction()
        {
            this.AddDefaultConfiguration()
              .AddDefaultService();
        }

        public DefaultFrameworkConstruction(Action<IConfigurationBuilder> configure)
        {
            this.AddDefaultConfiguration(configure)
              .AddDefaultService();
        }

        #endregion

    }
}