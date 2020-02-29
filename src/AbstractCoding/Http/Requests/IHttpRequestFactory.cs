using System.Net.Http;

namespace AbstractCoding.Http.Requests
{
    public interface IHttpRequestFactory
    {
        IHttpRequest CreateGetRequest(string requestUri, HttpClient httpClient);

        IHttpRequest CreatePostRequest(string requestUri, HttpClient httpClient, HttpContent httpContent);

        IHttpRequest CreatePatchRequest(string requestUri, HttpClient httpClient, HttpContent httpContent);

        IHttpRequest CreatePutRequest(string requestUri, HttpClient httpClient, HttpContent httpContent);
    }
}