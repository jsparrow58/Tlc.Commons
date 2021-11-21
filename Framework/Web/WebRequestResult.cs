using System.Net;

namespace SJ.Web
{
    public class WebRequestResult
    {
        public bool Successful => ErrorMessage.IsNullOrWhiteSpace() == false;

        public string ErrorMessage { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public string StatusDescription { get; set; }

        public string ContentType { get; set; }

        public WebHeaderCollection Headers { get; set; }

        public CookieCollection Cookies { get; set; }

        /// <summary>
        /// 服务器返回的原始内容
        /// </summary>
        public string RawServerResponse { get; set; }

        /// <summary>
        /// 服务器返回的对象
        /// </summary>
        public object ServerResponse { get; set; }
    }

    public class WebRequestResult<T> : WebRequestResult
    {
        public new T ServerResponse
        {
            get => (T)base.ServerResponse;
            set => base.ServerResponse = value;
        }
    }
}