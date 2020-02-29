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
                responseContent = await DeserializeContent<TResponseContentType>(response);
            }
            catch (Exception exception)
            {
                throw CreateHttpRequestException(exception);
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
                responseContent = await DeserializeContent<TResponseContentType>(response);
            }
            catch (Exception exception)
            {
                throw CreateHttpRequestException(exception);
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
                responseContent = await DeserializeContent<TResponseContentType>(response);
            }
            catch (Exception exception)
            {
                throw CreateHttpRequestException(exception);
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
                responseContent = await DeserializeContent<TResponseContentType>(response);
            }
            catch (Exception exception)
            {
                throw CreateHttpRequestException(exception);
            }

            return responseContent;
        }

        private static async Task<TResponseContentType> DeserializeContent<TResponseContentType>(
            HttpResponseMessage response)
        {
            var responseContentRaw = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResponseContentType>(responseContentRaw);
        }

        private static HttpRequestException CreateHttpRequestException(Exception innerException)
        {
            return new HttpRequestException("An exception occurred. See inner exception for details.", innerException);
        }
    }
}