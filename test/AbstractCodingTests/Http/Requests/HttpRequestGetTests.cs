using System;
using System.Net.Http;
using AbstractCoding.Http.Requests;
using Xunit;

namespace AbstractCodingTests.Http.Requests
{
    public class HttpRequestGetTests : HttpRequestTestBase
    {
        public HttpRequestGetTests() : base(HttpMethod.Get)
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

        private static Func<string, HttpClient, HttpRequestGet> LoadInitConstructor()
        {
            return (requestUri, httpClient) => new HttpRequestGet(requestUri, httpClient);
        }
    }
}