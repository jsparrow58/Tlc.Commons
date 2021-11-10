using System;
using Microsoft.Extensions.Configuration;
using Tlc.Framework.Extensions;

namespace Tlc.Framework.Construction
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
