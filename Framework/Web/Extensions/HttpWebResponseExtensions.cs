using System.IO;
using System.Net;

namespace SJ.Web
{
    public static class HttpWebResponseExtensions
    {
        public static WebRequestResult<TResponse> CreateResEntity<TResponse>(this HttpWebResponse serverResponse)
        {
            var result = new WebRequestResult<TResponse>
            {
                ContentType = serverResponse.ContentType,
                Headers = serverResponse.Headers,
                Cookies = serverResponse.Cookies,
                StatusCode = serverResponse.StatusCode,
                StatusDescription = serverResponse.StatusDescription
            };

            if (result.StatusCode == HttpStatusCode.OK)
                using (var responseStream = serverResponse.GetResponseStream())
                {
                    using (var streamReader = new StreamReader(responseStream))
                    {
                        result.RawServerResponse = streamReader.ReadToEnd();
                    }
                }

            return result;
        }
    }
}