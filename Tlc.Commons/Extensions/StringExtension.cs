using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tlc.Commons.Extensions
{
    public static class StringExtension
    {
        public static bool IsNullOrEmpty(this string content) => string.IsNullOrEmpty(content);
        public static bool IsNullOrWhiteSpace(this string content) => string.IsNullOrWhiteSpace(content);
    }
}
