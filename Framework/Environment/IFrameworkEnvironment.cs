namespace SJ.Environment
{
  public interface IFrameworkEnvironment
  {
      /// <summary>
      ///   The configuration of environment, typically Development or Production
      /// </summary>
      string Configuration { get; }

      /// <summary>
      ///   True is we are in a development (specifically, debuggable) environment
      /// </summary>
      bool IsDevelopment { get; }

      /// <summary>
      ///   Indicates if we are a mobile platform
      /// </summary>
      bool IsMobile { get; }
  }
}