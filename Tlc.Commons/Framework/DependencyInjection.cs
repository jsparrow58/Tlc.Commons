using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Tlc.Framework
{
    public static class DependencyInjection
    {
        public static IConfiguration Configuration => Framework.Provider?.GetService<IConfiguration>();
    }
}
