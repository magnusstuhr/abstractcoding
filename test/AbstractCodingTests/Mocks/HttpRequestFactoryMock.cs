using System;
using System.Linq.Expressions;
using System.Net.Http;
using AbstractCoding.Http.Requests;
using Moq;
using Moq.Language.Flow;

namespace AbstractCodingTests.Mocks
{
    internal static class HttpRequestFactoryMock
    {
        private static readonly Times TimesOnce = Times.Once();

        internal static void SetupCreateGetRequestReturns(
            this Mock<IHttpRequestFactory> httpRequestFactoryMock,
            string requestUri,
            HttpClient httpClient,
            IHttpRequest httpRequestToReturn)
        {
            httpRequestFactoryMock.SetupCreateGetRequest(requestUri, httpClient).Returns(httpRequestToReturn);
        }

        internal static void SetupCreatePostRequestReturns(
            this Mock<IHttpRequestFactory> httpRequestFactoryMock,
            string requestUri,
            HttpClient httpClient,
            HttpContent httpContent,
            IHttpRequest httpRequestToReturn)
        {
            httpRequestFactoryMock.SetupCreatePostRequest(requestUri, httpClient, httpContent)
                .Returns(httpRequestToReturn);
        }

        internal static void SetupCreatePatchRequestReturns(
            this Mock<IHttpRequestFactory> httpRequestFactoryMock,
            string requestUri,
            HttpClient httpClient,
            HttpContent httpContent,
            IHttpRequest httpRequestToReturn)
        {
            httpRequestFactoryMock.SetupCreatePatchRequest(requestUri, httpClient, httpContent)
                .Returns(httpRequestToReturn);
        }

        internal static void SetupCreatePutRequestReturns(
            this Mock<IHttpRequestFactory> httpRequestFactoryMock,
            string requestUri,
            HttpClient httpClient,
            HttpContent httpContent,
            IHttpRequest httpRequestToReturn)
        {
            httpRequestFactoryMock.SetupCreatePutRequest(requestUri, httpClient, httpContent)
                .Returns(httpRequestToReturn);
        }

        internal static void VerifyCreateGetRequestWasCalled(
            this Mock<IHttpRequestFactory> httpRequestFactoryMock,
            string requestUri,
            HttpClient httpClient)
        {
            httpRequestFactoryMock.Verify(LoadCreateGetRequest(requestUri, httpClient), TimesOnce);
        }

        internal static void VerifyCreatePostRequestWasCalled(
            this Mock<IHttpRequestFactory> httpRequestFactoryMock,
            string requestUri,
            HttpClient httpClient,
            HttpContent httpContent)
        {
            httpRequestFactoryMock.Verify(LoadCreatePostRequest(requestUri, httpClient, httpContent), TimesOnce);
        }

        internal static void VerifyCreatePatchRequestWasCalled(
            this Mock<IHttpRequestFactory> httpRequestFactoryMock,
            string requestUri,
            HttpClient httpClient,
            HttpContent httpContent)
        {
            httpRequestFactoryMock.Verify(LoadCreatePatchRequest(requestUri, httpClient, httpContent), TimesOnce);
        }

        internal static void VerifyCreatePutRequestWasCalled(
            this Mock<IHttpRequestFactory> httpRequestFactoryMock,
            string requestUri,
            HttpClient httpClient,
            HttpContent httpContent)
        {
            httpRequestFactoryMock.Verify(LoadCreatePutRequest(requestUri, httpClient, httpContent), TimesOnce);
        }

        private static ISetup<IHttpRequestFactory, IHttpRequest> SetupCreateGetRequest(
            this Mock<IHttpRequestFactory> httpRequestFactoryMock,
            string requestUri,
            HttpClient httpClient)
        {
            return httpRequestFactoryMock.Setup(LoadCreateGetRequest(requestUri, httpClient));
        }

        private static ISetup<IHttpRequestFactory, IHttpRequest> SetupCreatePostRequest(
            this Mock<IHttpRequestFactory> httpRequestFactoryMock,
            string requestUri,
            HttpClient httpClient,
            HttpContent httpContent)
        {
            return httpRequestFactoryMock.Setup(LoadCreatePostRequest(requestUri, httpClient, httpContent));
        }

        private static ISetup<IHttpRequestFactory, IHttpRequest> SetupCreatePatchRequest(
            this Mock<IHttpRequestFactory> httpRequestFactoryMock,
            string requestUri,
            HttpClient httpClient,
            HttpContent httpContent)
        {
            return httpRequestFactoryMock.Setup(LoadCreatePatchRequest(requestUri, httpClient, httpContent));
        }

        private static ISetup<IHttpRequestFactory, IHttpRequest> SetupCreatePutRequest(
            this Mock<IHttpRequestFactory> httpRequestFactoryMock,
            string requestUri,
            HttpClient httpClient,
            HttpContent httpContent)
        {
            return httpRequestFactoryMock.Setup(LoadCreatePutRequest(requestUri, httpClient, httpContent));
        }

        private static Expression<Func<IHttpRequestFactory, IHttpRequest>> LoadCreateGetRequest(
            string requestUri,
            HttpClient httpClient)
        {
            return httpRequestFactory => httpRequestFactory.CreateGetRequest(requestUri, httpClient);
        }

        private static Expression<Func<IHttpRequestFactory, IHttpRequest>> LoadCreatePostRequest(
            string requestUri,
            HttpClient httpClient,
            HttpContent httpContent)
        {
            return httpRequestFactory => httpRequestFactory.CreatePostRequest(requestUri, httpClient, httpContent);
        }

        private static Expression<Func<IHttpRequestFactory, IHttpRequest>> LoadCreatePatchRequest(
            string requestUri,
            HttpClient httpClient,
            HttpContent httpContent)
        {
            return httpRequestFactory => httpRequestFactory.CreatePatchRequest(requestUri, httpClient, httpContent);
        }

        private static Expression<Func<IHttpRequestFactory, IHttpRequest>> LoadCreatePutRequest(
            string requestUri,
            HttpClient httpClient,
            HttpContent httpContent)
        {
            return httpRequestFactory => httpRequestFactory.CreatePutRequest(requestUri, httpClient, httpContent);
        }
    }
}