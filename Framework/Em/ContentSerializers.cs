using System.Net.Mime;

namespace SJ.Em
{
    /// <summary>
    /// 内容序列化格式
    /// </summary>
    public enum ContentSerializers
    {
        /// <summary>
        /// 内容序列化格式是 Json
        /// </summary>
        Json = 1,

        /// <summary>
        /// 内容序列化格式是 XML
        /// </summary>
        Xml
    }

    public static class ContentSerializersExtension
    {
        public static string ToMimeString(this ContentSerializers serializers)
        {
            switch (serializers)
            {
                case ContentSerializers.Json: return "application/json";
                case ContentSerializers.Xml: return "application/xml";
                default: return MediaTypeNames.Application.Octet;
            }

          ;
        }
    }
}