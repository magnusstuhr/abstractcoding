using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AbstractCoding.Http.Requests;
using AbstractCodingTests.Mocks;
using AbstractCodingTests.Mocks.Http.Model;
using Moq;

namespace AbstractCodingTests.Http.Requests
{
    public class HttpRequestWithBodyTestBase : HttpRequestTestBase
    {
        internal HttpRequestWithBodyTestBase(HttpMethod httpMethod) : base(httpMethod)
        {
        }

        internal async Task Execute__ValidRequestUriAndHttpClientAndHttpContent__ExpectedCallsWereMade(
            string requestContent,
            Func<string, HttpClient, HttpContent, HttpRequestWithBody> loadHttpRequest)
        {
            // Arrange
            var baseAddress = CreateRandomHttpsUri();
            var requestUri = BuildRequestUri(baseAddress, CreateRandomString());
            var requestUriString = requestUri.ToString();
            var requestBodyContent = requestContent != null ? new StringContent(requestContent) : null;

            var httpMessageHandlerMockRequestConfig =
                new HttpMessageHandlerMockRequestConfig(HttpStatusCode.OK, requestUriString, requestContent);
            var httpClientMockConfig =
                new HttpMessageHandlerMockConfig(baseAddress, httpMessageHandlerMockRequestConfig);

            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            httpMessageHandlerMock.SetupSendAsyncReturns(httpClientMockConfig);

            var httpClient = httpMessageHandlerMock.CreateHttpClient(baseAddress);

            var httpRequest = loadHttpRequest.Invoke(requestUriString, httpClient, requestBodyContent);

            // Act
            await httpRequest.Execute();

            // Assert
            httpMessageHandlerMock.VerifySendAsyncWasCalled(requestUri, Method, requestBodyContent);
        }
    }
}