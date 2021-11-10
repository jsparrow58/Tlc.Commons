using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Tlc.Environment;

namespace Tlc.Framework.Construction
{
    public class FrameworkConstruction
    {
        #region Private fields
        
        protected IServiceCollection services;

        #endregion


        #region Public Properties

        public IServiceProvider Provider { get; protected set; }

        public IServiceCollection Services
        {
            get => services;
            set
            {
                services = value;
                if (services != null) Services.AddSingleton(Environment);
            }
        }

        public IFrameworkEnvironment Environment { get; protected set; }

        public IConfiguration Configuration { get; protected set; }

        #endregion

        public FrameworkConstruction(bool createServiceCollection = true)
        {
            Environment = new DefaultFrameworkEnvironment();
            if (createServiceCollection)
            {
                Services = new ServiceCollection();
            }
        }

        /// <summary>
        /// Builds the service collection into a service provider
        /// </summary>
        /// <param name="provider"></param>
        public void Build(IServiceProvider provider = null)
        {
            Provider = provider ?? Services.BuildServiceProvider();
        }

        public FrameworkConstruction UseHostedServices(IServiceCollection services)
        {
            Services = services;
            return this;
        }

        public FrameworkConstruction UseConfiguration(IConfiguration configuration)
        {
            Configuration = configuration;
            return this;
        }
    }
}
