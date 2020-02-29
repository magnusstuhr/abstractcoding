using System.Net.Http;
using System.Threading.Tasks;

namespace AbstractCoding.Http.Requests
{
    public class HttpRequestPut : HttpRequestWithBody
    {
        public HttpRequestPut(string requestUri, HttpClient httpClient, HttpContent httpContent = null) : base(
            requestUri, httpClient, httpContent)
        {
        }

        public override Task<HttpResponseMessage> Execute(HttpContent httpContent)
        {
            return Client.PutAsync(RequestUri, httpContent);
        }
    }
}