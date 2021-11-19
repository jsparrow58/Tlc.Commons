using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJ
{
    /// <summary>
    /// 反射方法扩展
    /// </summary>
    public static class ReflectionExtensions
    {
        /// <summary>
        /// 获取程序集的物理位置
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string FileLocation(this Type type) => type.Assembly.Location;

        /// <summary>
        /// 获取程序集的物理目录
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string FolderLocation(this Type type) => Path.GetDirectoryName(type.Assembly.Location);
    }
}
