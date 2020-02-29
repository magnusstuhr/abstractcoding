using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AbstractCoding.Http.Operations;
using AbstractCoding.Http.Requests;
using AbstractCodingTests.Mocks;
using AbstractCodingTests.TestExtensions;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace AbstractCodingTests.Http.Operations
{
    public class HttpRequestOperatorTests
    {
        private readonly Mock<IHttpRequestFactory> _httpRequestFactoryMock;
        private readonly HttpRequestOperator _httpRequestOperator;

        public HttpRequestOperatorTests()
        {
            _httpRequestFactoryMock = new Mock<IHttpRequestFactory>();
            _httpRequestOperator = new HttpRequestOperator(_httpRequestFactoryMock.Object);
        }

        [Fact]
        public async void GetAsync_HttpRequestReturnsContent_ReturnsExpectedContentAndExpectedCallsWereMade()
        {
            // Arrange
            var loadHttpRequest = LoadGetAsync();
            var loadHttpRequestFactoryMockSetup = LoadSetupCreateGetRequestReturns();
            var loadHttpRequestFactoryMockVerification = LoadCreateGetRequestVerification();

            // Arrange, Act & Assert
            await RequestAsync_HttpRequestReturnsContent_ReturnsExpectedContentAndExpectedCallsWereMade(
                loadHttpRequestFactoryMockSetup, loadHttpRequest, loadHttpRequestFactoryMockVerification);
        }

        [Fact]
        public async void GetAsync_HttpRequestThrows_ThrowsHttpRequestExceptionAndExpectedCallsWereMade()
        {
            // Arrange
            var loadHttpRequest = LoadGetAsync();
            var loadHttpRequestFactoryMockSetup = LoadSetupCreateGetRequestReturns();
            var loadHttpRequestFactoryMockVerification = LoadCreateGetRequestVerification();

            // Arrange, Act & Assert
            await RequestAsync_HttpRequestThrows_ReturnsExpectedContentAndExpectedCallsWereMade(
                loadHttpRequestFactoryMockSetup, loadHttpRequest, loadHttpRequestFactoryMockVerification);
        }

        [Fact]
        public async void
            PostAsync_HttpRequestReturnsContent_ReturnsExpectedContentAndExpectedCallsWereMade()
        {
            // Arrange
            var loadHttpRequest = LoadPostAsync();
            var loadHttpRequestFactoryMockSetup = LoadSetupCreatePostRequestReturns();
            var loadHttpRequestFactoryMockVerification = LoadCreatePostRequestVerification();

            // Arrange, Act & Assert
            await RequestAsync_HttpRequestReturnsContent_ReturnsExpectedContentAndExpectedCallsWereMade(
                loadHttpRequestFactoryMockSetup, loadHttpRequest, loadHttpRequestFactoryMockVerification);
        }

        [Fact]
        public async void PostAsync_HttpRequestThrows_ThrowsHttpRequestExceptionAndExpectedCallsWereMade()
        {
            // Arrange
            var loadHttpRequest = LoadPostAsync();
            var loadHttpRequestFactoryMockSetup = LoadSetupCreatePostRequestReturns();
            var loadHttpRequestFactoryMockVerification = LoadCreatePostRequestVerification();

            // Arrange, Act & Assert
            await RequestAsync_HttpRequestThrows_ReturnsExpectedContentAndExpectedCallsWereMade(
                loadHttpRequestFactoryMockSetup, loadHttpRequest, loadHttpRequestFactoryMockVerification);
        }

        [Fact]
        public async void
            PatchAsync_HttpRequestReturnsContent_ReturnsExpectedContentAndExpectedCallsWereMade()
        {
            // Arrange
            var loadHttpRequest = LoadPatchAsync();
            var loadHttpRequestFactoryMockSetup = LoadSetupCreatePatchRequestReturns();
            var loadHttpRequestFactoryMockVerification = LoadCreatePatchRequestVerification();

            // Arrange, Act & Assert
            await RequestAsync_HttpRequestReturnsContent_ReturnsExpectedContentAndExpectedCallsWereMade(
                loadHttpRequestFactoryMockSetup, loadHttpRequest, loadHttpRequestFactoryMockVerification);
        }

        [Fact]
        public async void PatchAsync_HttpRequestThrows_ThrowsHttpRequestExceptionAndExpectedCallsWereMade()
        {
            // Arrange
            var loadHttpRequest = LoadPatchAsync();
            var loadHttpRequestFactoryMockSetup = LoadSetupCreatePatchRequestReturns();
            var loadHttpRequestFactoryMockVerification = LoadCreatePatchRequestVerification();

            // Arrange, Act & Assert
            await RequestAsync_HttpRequestThrows_ReturnsExpectedContentAndExpectedCallsWereMade(
                loadHttpRequestFactoryMockSetup, loadHttpRequest, loadHttpRequestFactoryMockVerification);
        }

        [Fact]
        public async void
            PutAsync_HttpRequestReturnsContent_ReturnsExpectedContentAndExpectedCallsWereMade()
        {
            // Arrange
            var loadHttpRequest = LoadPutAsync();
            var loadHttpRequestFactoryMockSetup = LoadSetupCreatePutRequestReturns();
            var loadHttpRequestFactoryMockVerification = LoadCreatePutRequestVerification();

            // Arrange, Act & Assert
            await RequestAsync_HttpRequestReturnsContent_ReturnsExpectedContentAndExpectedCallsWereMade(
                loadHttpRequestFactoryMockSetup, loadHttpRequest, loadHttpRequestFactoryMockVerification);
        }

        [Fact]
        public async void PutAsync_HttpRequestThrows_ThrowsHttpRequestExceptionAndExpectedCallsWereMade()
        {
            // Arrange
            var loadHttpRequest = LoadPutAsync();
            var loadHttpRequestFactoryMockSetup = LoadSetupCreatePutRequestReturns();
            var loadHttpRequestFactoryMockVerification = LoadCreatePutRequestVerification();

            // Arrange, Act & Assert
            await RequestAsync_HttpRequestThrows_ReturnsExpectedContentAndExpectedCallsWereMade(
                loadHttpRequestFactoryMockSetup, loadHttpRequest, loadHttpRequestFactoryMockVerification);
        }

        private async Task RequestAsync_HttpRequestReturnsContent_ReturnsExpectedContentAndExpectedCallsWereMade(
            Action<Mock<IHttpRequestFactory>, string, HttpClient, HttpContent, IHttpRequest>
                loadHttpRequestFactoryMockSetup,
            Func<string, HttpClient, HttpContent, Task<Person>> loadHttpRequest,
            Action<Mock<IHttpRequestFactory>, string, HttpClient, HttpContent> loadHttpRequestFactoryMockVerification)
        {
            // Arrange
            var requestUri = CreateRandomString();
            var httpClient = new HttpClient();
            var personToReturn = CreateRandomPerson();
            var requestHttpContent = CreateRandomHttpContent();

            var httpRequestMock = new Mock<IHttpRequest>();
            var httpResponseMessageToReturn = CreateHttpResponseMessage(personToReturn);
            httpRequestMock.SetupExecuteReturns(httpResponseMessageToReturn);

            loadHttpRequestFactoryMockSetup.Invoke(_httpRequestFactoryMock, requestUri, httpClient, requestHttpContent,
                httpRequestMock.Object);

            // Act
            var actualResponseContent = await loadHttpRequest.Invoke(requestUri, httpClient, requestHttpContent);

            // Assert
            Assert.Equal(personToReturn, actualResponseContent);

            loadHttpRequestFactoryMockVerification.Invoke(_httpRequestFactoryMock, requestUri, httpClient,
                requestHttpContent);
            httpRequestMock.VerifyExecuteWasCalled();
        }

        private async Task RequestAsync_HttpRequestThrows_ReturnsExpectedContentAndExpectedCallsWereMade(
            Action<Mock<IHttpRequestFactory>, string, HttpClient, HttpContent, IHttpRequest>
                loadHttpRequestFactoryMockSetup,
            Func<string, HttpClient, HttpContent, Task<Person>> loadHttpRequest,
            Action<Mock<IHttpRequestFactory>, string, HttpClient, HttpContent> loadHttpRequestFactoryMockVerification)
        {
            // Arrange
            var requestUri = CreateRandomString();
            var httpClient = new HttpClient();
            var requestHttpContent = CreateRandomHttpContent();

            var httpRequestMock = new Mock<IHttpRequest>();
            var exceptionToThrow = CreateRandomException();
            httpRequestMock.SetupExecuteThrows(exceptionToThrow);

            loadHttpRequestFactoryMockSetup.Invoke(_httpRequestFactoryMock, requestUri, httpClient, requestHttpContent,
                httpRequestMock.Object);

            // Assert
            var exception =
                await Record.ExceptionAsync(() => loadHttpRequest.Invoke(requestUri, httpClient, requestHttpContent));

            // Assert
            exception.Verify<HttpRequestException>("An exception occurred. See inner exception for details.");

            var innerException = exception?.InnerException;
            innerException.Verify<Exception>(exceptionToThrow.Message);

            loadHttpRequestFactoryMockVerification.Invoke(_httpRequestFactoryMock, requestUri, httpClient,
                requestHttpContent);
            httpRequestMock.VerifyExecuteWasCalled();
        }

        private static Action<Mock<IHttpRequestFactory>, string, HttpClient, HttpContent>
            LoadCreateGetRequestVerification()
        {
            return (httpRequestFactoryMock, requestUri, httpClient, httpContent) =>
                httpRequestFactoryMock.VerifyCreateGetRequestWasCalled(requestUri, httpClient);
        }

        private static Action<Mock<IHttpRequestFactory>, string, HttpClient, HttpContent, IHttpRequest>
            LoadSetupCreateGetRequestReturns()
        {
            return (httpRequestFactoryMock, requestUri, httpClient, httpContent, httpRequestToReturn) =>
                httpRequestFactoryMock.SetupCreateGetRequestReturns(requestUri, httpClient, httpRequestToReturn);
        }

        private static Action<Mock<IHttpRequestFactory>, string, HttpClient, HttpContent>
            LoadCreatePostRequestVerification()
        {
            return (httpRequestFactoryMock, requestUri, httpClient, httpContent) =>
                httpRequestFactoryMock.VerifyCreatePostRequestWasCalled(requestUri, httpClient, httpContent);
        }

        private static Action<Mock<IHttpRequestFactory>, string, HttpClient, HttpContent, IHttpRequest>
            LoadSetupCreatePostRequestReturns()
        {
            return (httpRequestFactoryMock, requestUri, httpClient, httpContent, httpRequestToReturn) =>
                httpRequestFactoryMock.SetupCreatePostRequestReturns(requestUri, httpClient, httpContent,
                    httpRequestToReturn);
        }

        private static Action<Mock<IHttpRequestFactory>, string, HttpClient, HttpContent>
            LoadCreatePatchRequestVerification()
        {
            return (httpRequestFactoryMock, requestUri, httpClient, httpContent) =>
                httpRequestFactoryMock.VerifyCreatePatchRequestWasCalled(requestUri, httpClient, httpContent);
        }

        private static Action<Mock<IHttpRequestFactory>, string, HttpClient, HttpContent, IHttpRequest>
            LoadSetupCreatePatchRequestReturns()
        {
            return (httpRequestFactoryMock, requestUri, httpClient, httpContent, httpRequestToReturn) =>
                httpRequestFactoryMock.SetupCreatePatchRequestReturns(requestUri, httpClient, httpContent,
                    httpRequestToReturn);
        }

        private static Action<Mock<IHttpRequestFactory>, string, HttpClient, HttpContent>
            LoadCreatePutRequestVerification()
        {
            return (httpRequestFactoryMock, requestUri, httpClient, httpContent) =>
                httpRequestFactoryMock.VerifyCreatePutRequestWasCalled(requestUri, httpClient, httpContent);
        }

        private static Action<Mock<IHttpRequestFactory>, string, HttpClient, HttpContent, IHttpRequest>
            LoadSetupCreatePutRequestReturns()
        {
            return (httpRequestFactoryMock, requestUri, httpClient, httpContent, httpRequestToReturn) =>
                httpRequestFactoryMock.SetupCreatePutRequestReturns(requestUri, httpClient, httpContent,
                    httpRequestToReturn);
        }

        private Func<string, HttpClient, HttpContent, Task<Person>> LoadGetAsync()
        {
            return (requestUri, httpClient, httpContent) =>
                _httpRequestOperator.GetAsync<Person>(requestUri, httpClient);
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

        private static HttpResponseMessage CreateHttpResponseMessage<T>(T contentToReturn) where T : class
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = CreateHttpContent(contentToReturn)
            };
        }

        private static HttpContent CreateRandomHttpContent()
        {
            return CreateHttpContent(CreateRandomPerson());
        }

        private static HttpContent CreateHttpContent<T>(T contentToReturn) where T : class
        {
            return new StringContent(ConvertToJson(contentToReturn));
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