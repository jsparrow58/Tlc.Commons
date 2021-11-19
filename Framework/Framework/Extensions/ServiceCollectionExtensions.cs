using Microsoft.Extensions.DependencyInjection;
using SJ.Framework.Construction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJ.Framework
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 把构架加入到主机应用当中
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static FrameworkConstruction AddSJFramework(this IServiceCollection services)
        {
            Framework.Construction.UseHostedServices(services);
            return Framework.Construction;
        }
    }
}
