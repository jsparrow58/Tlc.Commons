using System.Net.Mime;

namespace SJ.Em
{
    /// <summary>
    ///   Content Serializers
    /// </summary>
    public enum ContentSerializers
  {
      /// <summary>
      ///   Content Serializer is json
      /// </summary>
      Json = 1,

      /// <summary>
      ///   Content Serializer is xml
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