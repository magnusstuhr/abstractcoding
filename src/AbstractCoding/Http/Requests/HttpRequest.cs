using System.Net.Http;
using System.Threading.Tasks;
using AbstractCoding.Extensions;

namespace AbstractCoding.Http.Requests
{
    public abstract class HttpRequest
    {
        protected readonly string RequestUri;
        protected readonly HttpClient Client;

        protected HttpRequest(string requestUri, HttpClient httpClient)
        {
            requestUri.ValidateIsNotNull(nameof(requestUri));
            httpClient.ValidateIsNotNull(nameof(httpClient));

            RequestUri = requestUri;
            Client = httpClient;
        }

        public abstract Task<HttpResponseMessage> Execute();
    }
}