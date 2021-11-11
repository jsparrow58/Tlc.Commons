using System.Linq;

namespace SJ.Extensions
{
    /// <summary>
    ///   数组扩展方法
    /// </summary>
    public static class ArrayExtensions
  {
      /// <summary>
      ///   在源对象数组后方添加新的数组
      /// </summary>
      /// <typeparam name="T">数组类型</typeparam>
      /// <param name="source">源数组</param>
      /// <param name="toAdd">需要添加的数组</param>
      /// <returns></returns>
      public static T[] Append<T>(this T[] source, params T[] toAdd)
    {
      return source.Concat(toAdd).ToArray();
    }

      /// <summary>
      ///   在源对象数组前方添加新的数组
      /// </summary>
      /// <typeparam name="T">数组类型</typeparam>
      /// <param name="source">源数组</param>
      /// <param name="toAdd">需要添加的数组</param>
      /// <returns></returns>
      public static T[] Prepend<T>(this T[] source, params T[] toAdd)
    {
      return toAdd.Append(source);
    }
  }
}