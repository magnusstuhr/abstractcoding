using System.Net.Http;
using System.Threading.Tasks;

namespace AbstractCoding.Http.Requests
{
    public abstract class HttpRequestWithBody : HttpRequest
    {
        protected readonly HttpContent Content;

        protected HttpRequestWithBody(string requestUri, HttpClient httpClient, HttpContent httpContent) : base(
            requestUri,
            httpClient)
        {
            Content = httpContent;
        }

        public abstract Task<HttpResponseMessage> Execute(HttpContent httpContent);

        public override Task<HttpResponseMessage> Execute()
        {
            return Execute(Content);
        }
    }
}