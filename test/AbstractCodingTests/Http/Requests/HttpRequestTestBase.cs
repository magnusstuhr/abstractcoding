using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AbstractCoding.Http.Requests;
using AbstractCodingTests.Mocks;
using AbstractCodingTests.Mocks.Http.Model;
using AbstractCodingTests.TestExtensions;
using Moq;
using Xunit;

namespace AbstractCodingTests.Http.Requests
{
    public class HttpRequestTestBase
    {
        internal readonly HttpMethod Method;

        internal HttpRequestTestBase(HttpMethod httpMethod)
        {
            Method = httpMethod;
        }

        internal async Task Execute__ValidRequestUriAndHttpClient__ExpectedCallsWereMade(
            Func<string, HttpClient, HttpRequest> loadHttpRequest)
        {
            // Arrange
            var baseAddress = CreateRandomHttpsUri();
            var requestUri = BuildRequestUri(baseAddress, CreateRandomString());
            var requestUriString = requestUri.ToString();

            var httpMessageHandlerMockRequestConfig =
                new HttpMessageHandlerMockRequestConfig(HttpStatusCode.OK, requestUriString);
            var httpClientMockConfig =
                new HttpMessageHandlerMockConfig(baseAddress, httpMessageHandlerMockRequestConfig);

            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            httpMessageHandlerMock.SetupSendAsyncReturns(httpClientMockConfig);

            var httpClient = httpMessageHandlerMock.CreateHttpClient(baseAddress);

            var httpRequest = loadHttpRequest.Invoke(requestUriString, httpClient);

            // Act
            await httpRequest.Execute();

            // Assert
            httpMessageHandlerMock.VerifySendAsyncWasCalled(requestUri, Method);
        }

        internal static void InitConstructor__RequestUriIsNull__ThrowsArgumentNullException(
            Func<string, HttpClient, HttpRequest> loadAct)
        {
            // Arrange
            const string expectedExceptionParamName = "requestUri";
            const string requestUri = null;
            var httpClient = new HttpClient();

            // Act & Assert
            InitConstructor_ArgumentIsNull_ThrowsArgumentNullException(() => loadAct.Invoke(requestUri, httpClient),
                expectedExceptionParamName);
        }

        internal static void InitConstructor__HttpClientIsNull__ThrowsArgumentNullException(
            Func<string, HttpClient, HttpRequest> loadAct)
        {
            // Arrange
            const string expectedExceptionParamName = "httpClient";
            var requestUri = CreateRandomString();
            const HttpClient httpClient = null;

            // Act & Assert
            InitConstructor_ArgumentIsNull_ThrowsArgumentNullException(() => loadAct.Invoke(requestUri, httpClient),
                expectedExceptionParamName);
        }

        internal static void InitConstructor_ArgumentIsNull_ThrowsArgumentNullException(
            Func<HttpRequest> loadAct,
            string expectedExceptionParamName)
        {
            // Act
            var exception = Record.Exception(loadAct);

            // Assert
            exception.VerifyArgumentNullException(expectedExceptionParamName);
        }

        internal static Uri BuildRequestUri(Uri baseAddress, string uriPath)
        {
            return new Uri(baseAddress, uriPath);
        }

        internal static Uri CreateRandomHttpsUri()
        {
            return new Uri($"https://{CreateRandomString()}.{CreateRandomString()}/");
        }

        internal static string CreateRandomString()
        {
            return Guid.NewGuid().ToString();
        }
    }
}