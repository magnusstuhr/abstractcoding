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
            TResponseContentType responseContent;

            try
            {
                var response = await httpClient.GetAsync(requestUri);
                var responseContentRaw = await response.Content.ReadAsStringAsync();
                responseContent = JsonConvert.DeserializeObject<TResponseContentType>(responseContentRaw);
            }
            catch (Exception exception)
            {
                throw new HttpRequestException("An exception occurred. See inner exception for details.", exception);
            }

            return responseContent;
        }

        public async Task<TResponseContentType> PostAsync<TResponseContentType>(string requestUri,
            HttpClient httpClient, HttpContent httpContent)
        {
            TResponseContentType responseContent;

            try
            {
                var response = await httpClient.PostAsync(requestUri, httpContent);
                var responseContentRaw = await response.Content.ReadAsStringAsync();
                responseContent = JsonConvert.DeserializeObject<TResponseContentType>(responseContentRaw);
            }
            catch (Exception exception)
            {
                throw new HttpRequestException("An exception occurred. See inner exception for details.", exception);
            }

            return responseContent;
        }

        public async Task<TResponseContentType> PatchAsync<TResponseContentType>(string requestUri,
            HttpClient httpClient, HttpContent httpContent)
        {
            TResponseContentType responseContent;

            try
            {
                var response = await httpClient.PatchAsync(requestUri, httpContent);
                var responseContentRaw = await response.Content.ReadAsStringAsync();
                responseContent = JsonConvert.DeserializeObject<TResponseContentType>(responseContentRaw);
            }
            catch (Exception exception)
            {
                throw new HttpRequestException("An exception occurred. See inner exception for details.", exception);
            }

            return responseContent;
        }

        public async Task<TResponseContentType> PutAsync<TResponseContentType>(string requestUri,
            HttpClient httpClient, HttpContent httpContent)
        {
            TResponseContentType responseContent;

            try
            {
                var response = await httpClient.PutAsync(requestUri, httpContent);
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