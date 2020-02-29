using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractCodingTests.Mocks.Http.Model
{
    public class HttpMessageHandlerMockConfig
    {
        public readonly Uri BaseAddress;
        public ICollection<HttpMessageHandlerMockRequestConfig> RequestConfigs;

        public HttpMessageHandlerMockConfig(Uri baseAddress, HttpMessageHandlerMockRequestConfig httpMessageHandlerMockRequestConfig,
            params HttpMessageHandlerMockRequestConfig[] additionalHttpMessageHandlerMockRequestConfigs)
        {
            BaseAddress = baseAddress;
            RequestConfigs = additionalHttpMessageHandlerMockRequestConfigs.ToList();
            RequestConfigs.Add(httpMessageHandlerMockRequestConfig);
        }
    }
}