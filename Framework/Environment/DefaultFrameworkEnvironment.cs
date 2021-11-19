using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace SJ.Environment
{
    public class DefaultFrameworkEnvironment : IFrameworkEnvironment
    {

        #region Default Constructor

        #endregion

        public string Configuration => IsDevelopment ? "Development" : "Production";

        public bool IsDevelopment =>
          Assembly.GetEntryAssembly()?.GetCustomAttribute<DebuggableAttribute>()?.IsJITTrackingEnabled == true;

        public bool IsMobile => RuntimeInformation.FrameworkDescription?.ToLower().Contains("mono") == true;
    }
}