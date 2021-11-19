using System.IO;
using System.Net;

namespace SJ.Web.Extensions
{
    public static class HttpWebResponseExtensions
    {
        public static ResEntity<TResponse> CreateResEntity<TResponse>(this HttpWebResponse serverResponse)
        {
            var result = new ResEntity<TResponse>
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