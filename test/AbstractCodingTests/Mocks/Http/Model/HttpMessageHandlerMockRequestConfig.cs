using System.Net;
using System.Net.Http;

namespace AbstractCodingTests.Mocks.Http.Model
{
    public class HttpMessageHandlerMockRequestConfig
    {
        public readonly HttpStatusCode StatusCode;
        public readonly string RequestUriSubPath;
        public readonly HttpContent RequestContent;

        public HttpMessageHandlerMockRequestConfig(HttpStatusCode statusCode, string requestUriSubPath,
            string requestContent = null)
        {
            StatusCode = statusCode;
            RequestUriSubPath = requestUriSubPath;
            RequestContent = new StringContent(requestContent ?? string.Empty);
        }
    }
}