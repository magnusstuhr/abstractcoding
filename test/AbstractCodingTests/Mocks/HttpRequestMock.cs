using System;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;
using AbstractCoding.Http.Requests;
using Moq;
using Moq.Language.Flow;

namespace AbstractCodingTests.Mocks
{
    internal static class HttpRequestMock
    {
        private static readonly Times TimesOnce = Times.Once();

        internal static void SetupExecuteReturns(this Mock<IHttpRequest> httpRequestMock,
            HttpResponseMessage httpResponseMessageToReturn)
        {
            SetupExecute(httpRequestMock).ReturnsAsync(httpResponseMessageToReturn);
        }
        internal static void SetupExecuteThrows(this Mock<IHttpRequest> httpRequestMock,
            Exception exceptionToThrow)
        {
            SetupExecute(httpRequestMock).Throws(exceptionToThrow);
        }

        internal static void VerifyExecuteWasCalled(this Mock<IHttpRequest> httpRequestMock)
        {
            httpRequestMock.Verify(LoadExecute(), TimesOnce);
        }

        private static ISetup<IHttpRequest, Task<HttpResponseMessage>> SetupExecute(Mock<IHttpRequest> httpRequestMock)
        {
            return httpRequestMock.Setup(LoadExecute());
        }

        private static Expression<Func<IHttpRequest, Task<HttpResponseMessage>>> LoadExecute()
        {
            return request => request.Execute();
        }
    }
}