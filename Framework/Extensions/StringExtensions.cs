namespace Tlc.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string content) => string.IsNullOrEmpty(content);
        public static bool IsNullOrWhiteSpace(this string content) => string.IsNullOrWhiteSpace(content);
    }
}
