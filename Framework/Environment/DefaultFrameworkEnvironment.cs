using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Tlc.Environment
{
    public class DefaultFrameworkEnvironment : IFrameworkEnvironment
    {
        public string Configuration => IsDevelopment ? "Development" : "Production";
        public bool IsDevelopment => Assembly.GetEntryAssembly()?.GetCustomAttribute<DebuggableAttribute>()?.IsJITTrackingEnabled == true;
        public bool IsMobile => RuntimeInformation.FrameworkDescription?.ToLower().Contains("mono") == true;

        #region Default Constructor

        public DefaultFrameworkEnvironment()
        {

        }

        #endregion
    }
}
