using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AbstractCoding.Http.Operations;
using AbstractCodingTests.Mocks;
using AbstractCodingTests.Mocks.Http.Model;
using AbstractCodingTests.TestExtensions;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace AbstractCodingTests.Http.Operations
{
    public class HttpRequestOperatorTests
    {
        public const HttpStatusCode HttpOk = HttpStatusCode.OK;

        private readonly HttpRequestOperator _httpRequestOperator;

        public HttpRequestOperatorTests()
        {
            _httpRequestOperator = new HttpRequestOperator();
        }

        [Fact]
        public async void GetAsync_ValidHttpClientAndRequestUri_ReturnsExpectedContentAndExpectedCallsWereMade()
        {
            // Arrange
            var expectedHttpMethod = HttpMethod.Get;
            var baseAddress = CreateRandomHttpsUri();
            var requestUri = BuildRequestUri(baseAddress, uriPath: CreateRandomString());
            var requestUriString = requestUri.ToString();
            var expectedResponseContent = CreateRandomPerson();

            var jsonContentToReturn = ConvertToJson(expectedResponseContent);

            var httpMessageHandlerMockRequestConfig =
                new HttpMessageHandlerMockRequestConfig(HttpOk, requestUriString,
                    jsonContentToReturn);
            var httpClientMockConfig =
                new HttpMessageHandlerMockConfig(baseAddress, httpMessageHandlerMockRequestConfig);

            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            httpMessageHandlerMock.SetupSendAsyncReturns(httpClientMockConfig);

            var httpClient = httpMessageHandlerMock.CreateHttpClient(baseAddress);

            // Act
            var actualResponseContent = await _httpRequestOperator.GetAsync<Person>(requestUriString, httpClient);

            // Assert
            Assert.Equal(expectedResponseContent, actualResponseContent);
            httpMessageHandlerMock.VerifySendAsyncWasCalled(requestUri, expectedHttpMethod);
        }

        [Fact]
        public async void GetAsync_ValidHttpClientAndRequestUri_ThrowsHttpRequestExceptionAndExpectedCallsWereMade()
        {
            // Arrange
            var expectedHttpMethod = HttpMethod.Get;
            var baseAddress = CreateRandomHttpsUri();
            var requestUri = BuildRequestUri(baseAddress, uriPath: CreateRandomString());
            var requestUriString = requestUri.ToString();
            var exceptionToThrow = CreateRandomException();

            var httpMessageHandlerMockRequestConfig =
                new HttpMessageHandlerMockRequestConfig(HttpOk, requestUriString);
            var httpClientMockConfig =
                new HttpMessageHandlerMockConfig(baseAddress, httpMessageHandlerMockRequestConfig);

            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            httpMessageHandlerMock.SetupSendAsyncThrows(httpClientMockConfig, exceptionToThrow);

            var httpClient = httpMessageHandlerMock.CreateHttpClient(baseAddress);

            // Act
            var exception =
                await Record.ExceptionAsync(() => _httpRequestOperator.GetAsync<Person>(requestUriString, httpClient));

            // Assert
            exception.Verify<HttpRequestException>("An exception occurred. See inner exception for details.");

            var innerException = exception?.InnerException;
            innerException.Verify<Exception>(exceptionToThrow.Message);

            httpMessageHandlerMock.VerifySendAsyncWasCalled(requestUri, expectedHttpMethod);
        }

        [Fact]
        public async void
            PostAsync_ValidHttpClientAndRequestUriAndRequestContent_ReturnsExpectedContentAndExpectedCallsWereMade()
        {
            // Arrange
            var expectedHttpMethod = HttpMethod.Post;
            var loadHttpRequest = LoadPostAsync();

            // Arrange, Act & Assert
            await
                RequestWithBodyAsync_ValidHttpClientAndRequestUriAndRequestBody_ReturnsExpectedContentAndExpectedCallsWereMade(
                    loadHttpRequest, expectedHttpMethod);
        }

        [Fact]
        public async void PostAsync_ValidHttpClientAndRequestUri_ThrowsHttpRequestExceptionAndExpectedCallsWereMade()
        {
            // Arrange
            var expectedHttpMethod = HttpMethod.Post;
            var loadHttpRequest = LoadPostAsync();

            // Arrange, Act & Assert
            await RequestWithBodyAsync_ValidHttpClientAndRequestUri_ThrowsHttpRequestExceptionAndExpectedCallsWereMade(
                loadHttpRequest, expectedHttpMethod);
        }

        [Fact]
        public async void
            PatchAsync_ValidHttpClientAndRequestUriAndRequestContent_ReturnsExpectedContentAndExpectedCallsWereMade()
        {
            // Arrange
            var expectedHttpMethod = HttpMethod.Patch;
            var loadHttpRequest = LoadPatchAsync();

            // Arrange, Act & Assert
            await
                RequestWithBodyAsync_ValidHttpClientAndRequestUriAndRequestBody_ReturnsExpectedContentAndExpectedCallsWereMade(
                    loadHttpRequest, expectedHttpMethod);
        }

        [Fact]
        public async void PatchAsync_ValidHttpClientAndRequestUri_ThrowsHttpRequestExceptionAndExpectedCallsWereMade()
        {
            // Arrange
            var expectedHttpMethod = HttpMethod.Patch;
            var loadHttpRequest = LoadPatchAsync();

            // Arrange, Act & Assert
            await RequestWithBodyAsync_ValidHttpClientAndRequestUri_ThrowsHttpRequestExceptionAndExpectedCallsWereMade(
                loadHttpRequest, expectedHttpMethod);
        }

        [Fact]
        public async void
            PutAsync_ValidHttpClientAndRequestUriAndRequestContent_ReturnsExpectedContentAndExpectedCallsWereMade()
        {
            // Arrange
            var expectedHttpMethod = HttpMethod.Put;
            var loadHttpRequest = LoadPutAsync();

            // Arrange, Act & Assert
            await
                RequestWithBodyAsync_ValidHttpClientAndRequestUriAndRequestBody_ReturnsExpectedContentAndExpectedCallsWereMade(
                    loadHttpRequest, expectedHttpMethod);
        }

        [Fact]
        public async void PutAsync_ValidHttpClientAndRequestUri_ThrowsHttpRequestExceptionAndExpectedCallsWereMade()
        {
            // Arrange
            var expectedHttpMethod = HttpMethod.Put;
            var loadHttpRequest = LoadPutAsync();

            // Arrange, Act & Assert
            await RequestWithBodyAsync_ValidHttpClientAndRequestUri_ThrowsHttpRequestExceptionAndExpectedCallsWereMade(
                loadHttpRequest, expectedHttpMethod);
        }

        private static async Task
            RequestWithBodyAsync_ValidHttpClientAndRequestUriAndRequestBody_ReturnsExpectedContentAndExpectedCallsWereMade(
                Func<string, HttpClient, HttpContent, Task<Person>> loadHttpRequest,
                HttpMethod expectedHttpMethod)
        {
            // Arrange
            var baseAddress = CreateRandomHttpsUri();
            var requestUri = BuildRequestUri(baseAddress, uriPath: CreateRandomString());
            var requestUriString = requestUri.ToString();
            var expectedResponseContent = CreateRandomPerson();

            var jsonContentToReturn = ConvertToJson(expectedResponseContent);
            var requestContent = new StringContent(jsonContentToReturn);

            var httpMessageHandlerMockRequestConfig =
                new HttpMessageHandlerMockRequestConfig(HttpOk, requestUriString,
                    jsonContentToReturn);
            var httpClientMockConfig =
                new HttpMessageHandlerMockConfig(baseAddress, httpMessageHandlerMockRequestConfig);

            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            httpMessageHandlerMock.SetupSendAsyncReturns(httpClientMockConfig);

            var httpClient = httpMessageHandlerMock.CreateHttpClient(baseAddress);

            // Act
            var actualResponseContent = await loadHttpRequest.Invoke(requestUriString, httpClient, requestContent);

            // Assert
            Assert.Equal(expectedResponseContent, actualResponseContent);
            httpMessageHandlerMock.VerifySendAsyncWasCalled(requestUri, expectedHttpMethod, requestContent);
        }

        private static async Task
            RequestWithBodyAsync_ValidHttpClientAndRequestUri_ThrowsHttpRequestExceptionAndExpectedCallsWereMade(
                Func<string, HttpClient, HttpContent, Task<Person>> loadHttpRequest, HttpMethod expectedHttpMethod)
        {
            // Arrange
            var baseAddress = CreateRandomHttpsUri();
            var requestUri = BuildRequestUri(baseAddress, uriPath: CreateRandomString());
            var requestUriString = requestUri.ToString();
            var exceptionToThrow = CreateRandomException();
            var expectedRequestContent = CreateRandomPerson();
            var expectedRequestContentJson = ConvertToJson(expectedRequestContent);
            var expectedRequestContentHttp = new StringContent(expectedRequestContentJson);

            var httpMessageHandlerMockRequestConfig =
                new HttpMessageHandlerMockRequestConfig(HttpOk, requestUriString, expectedRequestContentJson);
            var httpClientMockConfig =
                new HttpMessageHandlerMockConfig(baseAddress, httpMessageHandlerMockRequestConfig);

            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            httpMessageHandlerMock.SetupSendAsyncThrows(httpClientMockConfig, exceptionToThrow);

            var httpClient = httpMessageHandlerMock.CreateHttpClient(baseAddress);

            // Act
            var exception = await Record.ExceptionAsync(() =>
                loadHttpRequest.Invoke(requestUriString, httpClient, expectedRequestContentHttp));

            // Assert
            exception.Verify<HttpRequestException>("An exception occurred. See inner exception for details.");

            var innerException = exception?.InnerException;
            innerException.Verify<Exception>(exceptionToThrow.Message);

            httpMessageHandlerMock.VerifySendAsyncWasCalled(requestUri, expectedHttpMethod, expectedRequestContentHttp);
        }

        private Func<string, HttpClient, HttpContent, Task<Person>> LoadPostAsync()
        {
            return (requestUri, httpClient, httpContent) =>
                _httpRequestOperator.PostAsync<Person>(requestUri, httpClient, httpContent);
        }

        private Func<string, HttpClient, HttpContent, Task<Person>> LoadPatchAsync()
        {
            return (requestUri, httpClient, httpContent) =>
                _httpRequestOperator.PatchAsync<Person>(requestUri, httpClient, httpContent);
        }

        private Func<string, HttpClient, HttpContent, Task<Person>> LoadPutAsync()
        {
            return (requestUri, httpClient, httpContent) =>
                _httpRequestOperator.PutAsync<Person>(requestUri, httpClient, httpContent);
        }

        private static Person CreateRandomPerson()
        {
            return new Person
            {
                Id = CreateRandomString(),
                Name = CreateRandomString()
            };
        }

        private static string ConvertToJson<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        private static Uri BuildRequestUri(Uri baseAddress, string uriPath)
        {
            return new Uri(baseAddress, uriPath);
        }

        private static Uri CreateRandomHttpsUri()
        {
            return new Uri($"https://{CreateRandomString()}.{CreateRandomString()}/");
        }

        private static Exception CreateRandomException()
        {
            return new Exception(CreateRandomString());
        }

        private static string CreateRandomString()
        {
            return Guid.NewGuid().ToString();
        }

        public class Person : IEquatable<Person>
        {
            public string Id { get; set; }

            public string Name { get; set; }

            public override bool Equals(object obj)
            {
                return Equals(obj as Person);
            }

            public bool Equals(Person other)
            {
                if (ReferenceEquals(null, other))
                {
                    return false;
                }

                if (ReferenceEquals(this, other))
                {
                    return true;
                }

                return Id == other.Id && Name == other.Name;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Id, Name);
            }
        }
    }
}