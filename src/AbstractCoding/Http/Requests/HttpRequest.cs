using System.Net.Http;
using System.Threading.Tasks;
using AbstractCoding.Extensions;

namespace AbstractCoding.Http.Requests
{
    public abstract class HttpRequest : IHttpRequest
    {
        protected string RequestUri { get; }

        protected HttpClient Client { get; }

        public abstract Task<HttpResponseMessage> Execute();

        protected HttpRequest(string requestUri, HttpClient httpClient)
        {
            requestUri.ValidateIsNotNull(nameof(requestUri));
            httpClient.ValidateIsNotNull(nameof(httpClient));

            RequestUri = requestUri;
            Client = httpClient;
        }
    }
}