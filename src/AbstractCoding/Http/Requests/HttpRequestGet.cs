using System.Net.Http;
using System.Threading.Tasks;

namespace AbstractCoding.Http.Requests
{
    public class HttpRequestGet : HttpRequest
    {
        public HttpRequestGet(string requestUri, HttpClient httpClient) : base(requestUri, httpClient)
        {
        }

        public override Task<HttpResponseMessage> Execute()
        {
            return Client.GetAsync(RequestUri);
        }
    }
}