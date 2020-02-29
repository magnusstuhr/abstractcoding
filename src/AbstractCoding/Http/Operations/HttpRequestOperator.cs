using System;
using System.Net.Http;
using System.Threading.Tasks;
using AbstractCoding.Http.Requests;
using Newtonsoft.Json;

namespace AbstractCoding.Http.Operations
{
    public class HttpRequestOperator
    {
        public async Task<TResponseContentType> GetAsync<TResponseContentType>(string requestUri,
            HttpClient httpClient)
        {
            var httpRequest = new HttpRequestGet(requestUri, httpClient);

            return await ExecuteRequest<TResponseContentType>(httpRequest);
        }

        public async Task<TResponseContentType> PostAsync<TResponseContentType>(string requestUri,
            HttpClient httpClient, HttpContent httpContent)
        {
            var httpRequest = new HttpRequestPost(requestUri, httpClient, httpContent);

            return await ExecuteRequest<TResponseContentType>(httpRequest);
        }

        public async Task<TResponseContentType> PatchAsync<TResponseContentType>(string requestUri,
            HttpClient httpClient, HttpContent httpContent)
        {
            var httpRequest = new HttpRequestPatch(requestUri, httpClient, httpContent);

            return await ExecuteRequest<TResponseContentType>(httpRequest);
        }

        public async Task<TResponseContentType> PutAsync<TResponseContentType>(string requestUri,
            HttpClient httpClient, HttpContent httpContent)
        {
            var httpRequest = new HttpRequestPut(requestUri, httpClient, httpContent);

            return await ExecuteRequest<TResponseContentType>(httpRequest);
        }

        private static async Task<TResponseContentType> ExecuteRequest<TResponseContentType>(HttpRequest httpRequest)
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