using System.Net.Http;
using System.Threading.Tasks;

namespace AbstractCoding.Http.Requests
{
    public class HttpRequestPost : HttpRequestWithBody
    {
        public HttpRequestPost(string requestUri, HttpClient httpClient, HttpContent httpContent = null) : base(
            requestUri, httpClient, httpContent)
        {
        }

        public override Task<HttpResponseMessage> Execute(HttpContent httpContent)
        {
            return Client.PostAsync(RequestUri, httpContent);
        }
    }
}