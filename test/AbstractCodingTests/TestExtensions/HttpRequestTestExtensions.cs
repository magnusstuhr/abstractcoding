using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AbstractCodingTests.TestExtensions
{
    internal static class HttpRequestTestExtensions
    {
        internal static void AddRequestHeaders(this HttpClient httpClient,
            IDictionary<string, IEnumerable<string>> requestHeaders)
        {
            foreach (var (requestHeaderName, requestHeaderValue) in requestHeaders)
            {
                httpClient.DefaultRequestHeaders.Add(requestHeaderName, requestHeaderValue);
            }
        }

        internal static bool MatchesUri(this HttpRequestMessage httpRequestMessage, Uri requestUri)
        {
            return MatchesUri(httpRequestMessage.RequestUri, requestUri);
        }

        internal static async Task<bool> MatchesContent(this HttpRequestMessage httpRequestMessage,
            HttpContent httpContent)
        {
            return await GetStringFromHttpContent(httpContent) ==
                   await GetStringFromHttpContent(httpRequestMessage.Content);
        }

        internal static bool ContainsHeaders(this HttpRequestMessage httpRequestMessage,
            IDictionary<string, IEnumerable<string>> httpRequestHeaders)
        {
            var isExpectedRequestHeadersNull = httpRequestHeaders is null;

            var containsHeaders = isExpectedRequestHeadersNull;
            if (!isExpectedRequestHeadersNull)
            {
                var actualHttpRequestHeaders = httpRequestMessage.Headers;
                foreach (var (httpRequestHeaderName, httpRequestHeaderValue) in httpRequestHeaders)
                {
                    containsHeaders =
                        actualHttpRequestHeaders.ContainsHeader(httpRequestHeaderName, httpRequestHeaderValue);

                    if (!containsHeaders)
                    {
                        break;
                    }
                }
            }

            return containsHeaders;
        }

        private static bool ContainsHeader(this HttpRequestHeaders httpRequestHeaders,
            string httpRequestHeaderName, IEnumerable<string> httpRequestHeaderValue)
        {
            var actualHttpRequestHeader = GetHttpRequestHeader(httpRequestHeaders, httpRequestHeaderName);
            var actualHttpRequestHeaderValue = actualHttpRequestHeader.Value;

            return ContainsAllValues(httpRequestHeaderValue, actualHttpRequestHeaderValue);
        }

        private static bool MatchesUri(Uri uri1, Uri uri2)
        {
            return GetRequestUriString(uri1) == GetRequestUriString(uri2);
        }

        private static KeyValuePair<string, IEnumerable<string>> GetHttpRequestHeader(
            HttpRequestHeaders httpRequestHeaders, string httpRequestHeaderName)
        {
            return httpRequestHeaders.FirstOrDefault(keyValuePair =>
                keyValuePair.Key == httpRequestHeaderName);
        }

        private static async Task<string> GetStringFromHttpContent(HttpContent httpContent)
        {
            return await httpContent.ReadAsStringAsync();
        }

        private static bool ContainsAllValues<T>(IEnumerable<T> source, IEnumerable<T> target)
        {
            return source is null || target != null && source.All(target.Contains);
        }

        private static string GetRequestUriString(Uri uri)
        {
            return uri.ToString();
        }
    }
}