using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AbstractCodingTests.Mocks.Http.Model;
using AbstractCodingTests.TestExtensions;
using Moq;
using Moq.Language.Flow;
using Moq.Protected;

namespace AbstractCodingTests.Mocks
{
    public static class HttpMessageHandlerMock
    {
        private const string SendAsyncMethodName = "SendAsync";

        private static readonly Times TimesOnce = Times.Once();

        public static void SetupSendAsyncReturns(this Mock<HttpMessageHandler> httpMessagerHandlerMock,
            HttpMessageHandlerMockConfig httpMessageHandlerMockConfig)
        {
            Action<Uri, HttpMessageHandlerMockRequestConfig> loadHttpMessageHandlerMockSetup =
                httpMessagerHandlerMock.SetupSendAsyncReturns;

            SetupSendAsyncOutcome(httpMessageHandlerMockConfig, loadHttpMessageHandlerMockSetup);
        }

        public static void SetupSendAsyncThrows(this Mock<HttpMessageHandler> httpMessagerHandlerMock,
            HttpMessageHandlerMockConfig httpMessageHandlerMockConfig, Exception exceptionToThrow)
        {
            void LoadHttpMessageHandlerMockSetup(Uri baseAddress,
                HttpMessageHandlerMockRequestConfig httpMessageHandlerMockRequestConfig) =>
                httpMessagerHandlerMock.SetupSendAsyncThrows(baseAddress, httpMessageHandlerMockRequestConfig,
                    exceptionToThrow);

            SetupSendAsyncOutcome(httpMessageHandlerMockConfig, LoadHttpMessageHandlerMockSetup);
        }

        public static void VerifySendAsyncWasCalled(
            this Mock<HttpMessageHandler> httpMessagerHandlerMock,
            Uri requestUri,
            HttpMethod httpMethod,
            HttpContent requestBodyContent = null,
            IDictionary<string, IEnumerable<string>> requestHeaders = null,
            Times? times = null)
        {
            var timesCalled = CalculateExpectedTimes(times);

            httpMessagerHandlerMock.Protected().Verify<Task<HttpResponseMessage>>(SendAsyncMethodName, timesCalled,
                ItExpr.Is<HttpRequestMessage>(httpRequestMessage =>
                    httpRequestMessage.Method == httpMethod &&
                    httpRequestMessage.MatchesUri(requestUri) &&
                    (requestBodyContent == null || httpRequestMessage.MatchesContent(requestBodyContent).Result) &&
                    httpRequestMessage.ContainsHeaders(requestHeaders)
                ), ItExpr.IsAny<CancellationToken>());
        }

        public static HttpClient CreateHttpClient(this Mock<HttpMessageHandler> httpMessageHandlerMock, Uri baseAddress,
            IDictionary<string, IEnumerable<string>> httpClientRequestHeaders = null)
        {
            var httpClient = new HttpClient(httpMessageHandlerMock.Object)
            {
                BaseAddress = baseAddress
            };

            if (httpClientRequestHeaders != null)
            {
                httpClient.AddRequestHeaders(httpClientRequestHeaders);
            }

            return httpClient;
        }

        private static void SetupSendAsyncReturns(this Mock<HttpMessageHandler> httpMessagerHandlerMock,
            Uri baseAddress, HttpMessageHandlerMockRequestConfig httpMessageHandlerMockRequestConfig)
        {
            var httpResponseMessageToReturn = CreateHttpResponseMessage(httpMessageHandlerMockRequestConfig);

            void LoadMockSetup(Uri requestUri) =>
                httpMessagerHandlerMock.SetupSendAsync(requestUri)
                    .ReturnsAsync(httpResponseMessageToReturn)
                    .Verifiable();

            SetupSendAsyncOutcome(baseAddress, httpMessageHandlerMockRequestConfig, LoadMockSetup);
        }

        private static void SetupSendAsyncThrows(this Mock<HttpMessageHandler> httpMessagerHandlerMock,
            Uri baseAddress, HttpMessageHandlerMockRequestConfig httpMessageHandlerMockRequestConfig,
            Exception exceptionToThrow)
        {
            void LoadMockSetup(Uri requestUri) =>
                httpMessagerHandlerMock.SetupSendAsync(requestUri)
                    .Throws(exceptionToThrow)
                    .Verifiable();

            SetupSendAsyncOutcome(baseAddress, httpMessageHandlerMockRequestConfig, LoadMockSetup);
        }

        private static void SetupSendAsyncOutcome(HttpMessageHandlerMockConfig httpMessageHandlerMockConfig,
            Action<Uri, HttpMessageHandlerMockRequestConfig> loadHttpMessageHandlerMockSetup)
        {
            var baseAddress = httpMessageHandlerMockConfig.BaseAddress;

            foreach (var httpMessageHandlerMockRequestConfig in httpMessageHandlerMockConfig.RequestConfigs)
            {
                loadHttpMessageHandlerMockSetup.Invoke(baseAddress, httpMessageHandlerMockRequestConfig);
            }
        }

        private static void SetupSendAsyncOutcome(Uri baseAddress,
            HttpMessageHandlerMockRequestConfig httpMessageHandlerMockRequestConfig, Action<Uri> loadMockSetup)
        {
            var requestUri = new Uri(baseAddress, httpMessageHandlerMockRequestConfig.RequestUriSubPath);

            loadMockSetup.Invoke(requestUri);
        }

        private static ISetup<HttpMessageHandler, Task<HttpResponseMessage>> SetupSendAsync(
            this Mock<HttpMessageHandler> httpMessagerHandlerMock, Uri requestUri)
        {
            return httpMessagerHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    SendAsyncMethodName,
                    ItExpr.Is(RequestsWhereUrisMatch(requestUri)),
                    ItExpr.IsAny<CancellationToken>()
                );
        }

        private static Expression<Func<HttpRequestMessage, bool>> RequestsWhereUrisMatch(Uri requestUri)
        {
            return httpRequestMessage => httpRequestMessage.MatchesUri(requestUri);
        }

        private static HttpResponseMessage CreateHttpResponseMessage(
            HttpMessageHandlerMockRequestConfig httpMessageHandlerMockRequestConfig)
        {
            return new HttpResponseMessage
            {
                StatusCode = httpMessageHandlerMockRequestConfig.StatusCode,
                Content = httpMessageHandlerMockRequestConfig.RequestContent
            };
        }

        private static Times CalculateExpectedTimes(Times? times)
        {
            return times ?? TimesOnce;
        }
    }
}