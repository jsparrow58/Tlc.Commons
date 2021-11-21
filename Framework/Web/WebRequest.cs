using Newtonsoft.Json;
using SJ.Em;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SJ.Web
{
    public static class WebRequest
    {
        /// <summary>
        /// 发送Get请求返回请求原始信息
        /// </summary>
        /// <remarks>IMPORTANT: Remember to close the returned <see cref="HttpWebResponse" /> stream once done</remarks>
        /// <param name="url">请求地址</param>
        /// <param name="configRequest">允许在发送前自定义请求</param>
        /// <param name="bearerToken">在特殊情况下，请求需要发送bearerToken,以表明请求身份</param>
        /// <returns></returns>
        public static async Task<HttpWebResponse> GetAsync(string url,
            ContentSerializers sendType = ContentSerializers.Json,
            ContentSerializers returnType = ContentSerializers.Json,
            Action<HttpWebRequest> configRequest = null, string bearerToken = null)
        {
            var request = System.Net.WebRequest.CreateHttp(url);
            request.Method = WebRequestMethods.Http.Get;
            request.ContentType = sendType.ToMimeString();
            request.Accept = returnType.ToMimeString();

            // 请求Token
            if (!bearerToken.IsNullOrWhiteSpace())
                request.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {bearerToken}");
            // 其它的自定义设置
            configRequest?.Invoke(request);

            try
            {
                return await request.GetResponseAsync() as HttpWebResponse;
            }
            catch (WebException e)
            {
                if (e.Response is HttpWebResponse response) return response;
                throw;
            }
        }

        /// <summary>
        /// 发送Get请求返回请求原始信息
        /// </summary>
        /// <remarks>IMPORTANT: Remember to close the returned <see cref="HttpWebResponse" /> stream once done</remarks>
        /// <param name="url">请求地址</param>
        /// <param name="configRequest">允许在发送前自定义请求</param>
        /// <param name="bearerToken">在特殊情况下，请求需要发送bearerToken,以表明请求身份</param>
        /// <returns></returns>
        public static async Task<WebRequestResult<TResponse>> GetAsync<TResponse>(string url,
            ContentSerializers sendType = ContentSerializers.Json,
            ContentSerializers returnType = ContentSerializers.Json,
            Action<HttpWebRequest> configRequest = null, string bearerToken = null)
        {
            HttpWebResponse serverResponse;

            try
            {
                serverResponse = await GetAsync(url, sendType, returnType, configRequest, bearerToken);
            }
            catch (Exception e)
            {
                return new WebRequestResult<TResponse>() { ErrorMessage = e.Message };
            }

            var result = serverResponse.CreateResEntity<TResponse>();

            if (result.StatusCode != HttpStatusCode.OK) return result;

            if (result.RawServerResponse.IsNullOrEmpty()) return result;

            try
            {
                if (!serverResponse.ContentType.ToLower().Contains(returnType.ToMimeString().ToLower()))
                {
                    result.ErrorMessage =
                      $"服务器返回的格式不支持，接受的格式 {returnType.ToMimeString()}, 收到的格式 {serverResponse.ContentType}";
                    return result;
                }

                if (returnType == ContentSerializers.Json)
                {
                    result.ServerResponse = JsonConvert.DeserializeObject<TResponse>(result.RawServerResponse);
                }
                else if (returnType == ContentSerializers.Xml)
                {
                    var xmlSerializer = new XmlSerializer(typeof(TResponse));

                    using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(result.RawServerResponse)))
                    {
                        result.ServerResponse = (TResponse)xmlSerializer.Deserialize(memoryStream);
                    }
                }
                else
                {
                    result.ErrorMessage = "未知的返回类型，不能序列化服务器返回内容";
                }
            }
            catch (Exception)
            {
                result.ErrorMessage = "不能将服务器端返回的对象反序列化。";
                return result;
            }

            return result;
        }

        /// <summary>
        /// 发送Post请求 返回原始信息
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="content">post请求时发送的内容</param>
        /// <param name="sendType">发送内容类型</param>
        /// <param name="returnType">返回内容类型</param>
        /// <param name="configRequest">允许在发送前自定义请求</param>
        /// <param name="bearerToken">在特殊情况下，请求需要发送bearerToken,以表明请求身份</param>
        /// <returns></returns>
        public static async Task<HttpWebResponse> PostAsync(string url, object content = null,
        ContentSerializers sendType = ContentSerializers.Json,
        ContentSerializers returnType = ContentSerializers.Json, Action<HttpWebRequest> configRequest = null,
        string bearerToken = null)
        {
            var request = System.Net.WebRequest.CreateHttp(url);
            request.Method = WebRequestMethods.Http.Post;
            request.ContentType = sendType.ToMimeString();
            request.Accept = returnType.ToMimeString();

            if (!bearerToken.IsNullOrWhiteSpace())
                request.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {bearerToken}");

            if (content == null)
            {
                request.ContentLength = 0;
            }
            else
            {
                var contentString = string.Empty;

                if (sendType == ContentSerializers.Json)
                {
                    contentString = JsonConvert.SerializeObject(content);
                }
                else if (sendType == ContentSerializers.Xml)
                {
                    var xmlSerializer = new XmlSerializer(content.GetType());

                    using (var stringWriter = new StringWriter())
                    {
                        xmlSerializer.Serialize(stringWriter, content);
                        contentString = stringWriter.ToString();
                    }
                }

                using (var requestStream = await request.GetRequestStreamAsync())
                {
                    using (var streamWriter = new StreamWriter(requestStream))
                    {
                        await streamWriter.WriteAsync(contentString);
                    }
                }
            }

            try
            {
                return await request.GetResponseAsync() as HttpWebResponse;
            }
            catch (WebException e)
            {
                if (e.Response is HttpWebResponse response) return response;
                throw;
            }
        }

        /// <summary>
        /// 带返回类型的Post请求
        /// </summary>
        /// <typeparam name="TResponse">返回类型</typeparam>
        /// <param name="url">请求地址</param>
        /// <param name="content">请求内容</param>
        /// <param name="sendType">发送类型</param>
        /// <param name="returnType">返回类型</param>
        /// <param name="configureRequest">自定义请求参数</param>
        /// <param name="bearerToken">用户签名</param>
        /// <returns>any</returns>
        public static async Task<WebRequestResult<TResponse>> PostAsync<TResponse>(string url,
          object content = null,
          ContentSerializers sendType = ContentSerializers.Json,
          ContentSerializers returnType = ContentSerializers.Xml,
          Action<HttpWebRequest> configureRequest = null,
          string bearerToken = null)
        {
            HttpWebResponse serverResponse;

            try
            {
                serverResponse = await PostAsync(url, content, sendType, returnType, configureRequest, bearerToken);
            }
            catch (Exception e)
            {
                return new WebRequestResult<TResponse> { ErrorMessage = e.Message };
            }

            var result = serverResponse.CreateResEntity<TResponse>();

            if (result.StatusCode != HttpStatusCode.OK) return result;

            if (result.RawServerResponse.IsNullOrEmpty()) return result;

            try
            {
                if (!serverResponse.ContentType.ToLower().Contains(returnType.ToMimeString().ToLower()))
                {
                    result.ErrorMessage =
                      $"服务器返回的格式不支持，接受的格式 {returnType.ToMimeString()}, 收到的格式 {serverResponse.ContentType}";
                    return result;
                }

                if (returnType == ContentSerializers.Json)
                {
                    result.ServerResponse = JsonConvert.DeserializeObject<TResponse>(result.RawServerResponse);
                }
                else if (returnType == ContentSerializers.Xml)
                {
                    var xmlSerializer = new XmlSerializer(typeof(TResponse));

                    using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(result.RawServerResponse)))
                    {
                        result.ServerResponse = (TResponse)xmlSerializer.Deserialize(memoryStream);
                    }
                }
                else
                {
                    result.ErrorMessage = "未知的返回类型，不能序列化服务器返回内容";
                }
            }
            catch (Exception)
            {
                result.ErrorMessage = "不能将服务器端返回的对象反序列化。";
                return result;
            }

            return result;
        }
    }
}