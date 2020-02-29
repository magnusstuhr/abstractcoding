using System;
using System.Net.Http;
using AbstractCoding.Http.Requests;
using Xunit;

namespace AbstractCodingTests.Http.Requests
{
    public class HttpRequestPatchTests : HttpRequestWithBodyTestBase
    {
        public HttpRequestPatchTests() : base(HttpMethod.Patch)
        {
        }

        [Fact]
        public async void Execute_ValidRequestUriAndHttpClient_ExpectedCallsWereMade()
        {
            // Arrange
            var loadInitConstructor = LoadInitConstructor();

            // Arrange, Act & Assert
            await Execute__ValidRequestUriAndHttpClient__ExpectedCallsWereMade(loadInitConstructor);
        }

        [Fact]
        public async void Execute_ValidRequestUriAndHttpClientAndHttpContent_ExpectedCallsWereMade()
        {
            // Arrange
            var requestContent = CreateRandomString();
            var loadInitConstructor = LoadInitConstructorWithContent();

            // Arrange, Act & Assert
            await Execute__ValidRequestUriAndHttpClientAndHttpContent__ExpectedCallsWereMade(requestContent,
                loadInitConstructor);
        }

        [Fact]
        public void InitConstructor_RequestUriIsNull_ThrowsArgumentNullException()
        {
            // Arrange, Act & Assert
            InitConstructor__RequestUriIsNull__ThrowsArgumentNullException(LoadInitConstructor());
        }

        [Fact]
        public void InitConstructor_HttpClientIsNull_ThrowsArgumentNullException()
        {
            // Arrange, Act & Assert
            InitConstructor__HttpClientIsNull__ThrowsArgumentNullException(LoadInitConstructor());
        }

        private static Func<string, HttpClient, HttpRequestPatch> LoadInitConstructor()
        {
            return (requestUri, httpClient) => new HttpRequestPatch(requestUri, httpClient);
        }

        private static Func<string, HttpClient, HttpContent, HttpRequestPatch> LoadInitConstructorWithContent()
        {
            return (requestUri, httpClient, httpContent) => new HttpRequestPatch(requestUri, httpClient, httpContent);
        }
    }
}