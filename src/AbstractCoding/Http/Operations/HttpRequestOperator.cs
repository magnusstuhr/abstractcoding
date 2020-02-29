using System;
using System.Net.Http;
using System.Threading.Tasks;
using AbstractCoding.Http.Requests;
using Newtonsoft.Json;

namespace AbstractCoding.Http.Operations
{
    public class HttpRequestOperator
    {
        private readonly IHttpRequestFactory _httpRequestFactory;

        public HttpRequestOperator(IHttpRequestFactory httpRequestFactory)
        {
            _httpRequestFactory = httpRequestFactory;
        } 

        public async Task<TResponseContentType> GetAsync<TResponseContentType>(string requestUri,
            HttpClient httpClient)
        {
            var httpRequest = _httpRequestFactory.CreateGetRequest(requestUri, httpClient);

            return await ExecuteRequest<TResponseContentType>(httpRequest);
        }

        public async Task<TResponseContentType> PostAsync<TResponseContentType>(string requestUri,
            HttpClient httpClient, HttpContent httpContent)
        {
            var httpRequest = _httpRequestFactory.CreatePostRequest(requestUri, httpClient, httpContent);

            return await ExecuteRequest<TResponseContentType>(httpRequest);
        }

        public async Task<TResponseContentType> PatchAsync<TResponseContentType>(string requestUri,
            HttpClient httpClient, HttpContent httpContent)
        {
            var httpRequest = _httpRequestFactory.CreatePatchRequest(requestUri, httpClient, httpContent);

            return await ExecuteRequest<TResponseContentType>(httpRequest);
        }

        public async Task<TResponseContentType> PutAsync<TResponseContentType>(string requestUri,
            HttpClient httpClient, HttpContent httpContent)
        {
            var httpRequest = _httpRequestFactory.CreatePutRequest(requestUri, httpClient, httpContent);

            return await ExecuteRequest<TResponseContentType>(httpRequest);
        }

        private static async Task<TResponseContentType> ExecuteRequest<TResponseContentType>(IHttpRequest httpRequest)
        {
            TResponseContentType responseContent;

            try
            {
                var response = await httpRequest.Execute();
                var responseContentRaw = await response.Content.ReadAsStringAsync();
                responseContent = JsonConvert.DeserializeObject<TResponseContentType>(responseContentRaw);
            }
            catch (Exception exception)
            {
                throw new HttpRequestException("An exception occurred. See inner exception for details.", exception);
            }

            return responseContent;
        }
    }
}