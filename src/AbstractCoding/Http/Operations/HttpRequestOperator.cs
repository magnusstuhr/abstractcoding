using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AbstractCoding.Http.Operations
{
    public class HttpRequestOperator
    {
        public async Task<TResponseContentType> GetAsync<TResponseContentType>(string requestUri,
            HttpClient httpClient)
        {
            Task<HttpResponseMessage> LoadHttpResponse() => httpClient.GetAsync(requestUri);

            return await RequestAsync<TResponseContentType>(LoadHttpResponse);
        }

        public async Task<TResponseContentType> PostAsync<TResponseContentType>(string requestUri,
            HttpClient httpClient, HttpContent httpContent)
        {
            Task<HttpResponseMessage> LoadHttpResponse() => httpClient.PostAsync(requestUri, httpContent);

            return await RequestAsync<TResponseContentType>(LoadHttpResponse);
        }

        public async Task<TResponseContentType> PatchAsync<TResponseContentType>(string requestUri,
            HttpClient httpClient, HttpContent httpContent)
        {
            Task<HttpResponseMessage> LoadHttpResponse() => httpClient.PatchAsync(requestUri, httpContent);

            return await RequestAsync<TResponseContentType>(LoadHttpResponse);
        }

        public async Task<TResponseContentType> PutAsync<TResponseContentType>(string requestUri,
            HttpClient httpClient, HttpContent httpContent)
        {
            Task<HttpResponseMessage> LoadHttpResponse() => httpClient.PutAsync(requestUri, httpContent);

            return await RequestAsync<TResponseContentType>(LoadHttpResponse);
        }

        public async Task<TResponseContentType> RequestAsync<TResponseContentType>(
            Func<Task<HttpResponseMessage>> loadHttpResponse)
        {
            TResponseContentType responseContent;

            try
            {
                var response = await loadHttpResponse.Invoke();
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