using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using SEBTask.Models;
using SEBTask.Services.Base;
using SEBTask.Services.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SEBTask.Services
{
    public class ExchangeRateService : ApiClient, IExchangeRateService
    {
        private static MemoryCacheEntryOptions memoryCacheEntryOptions;

        public ExchangeRateService(HttpClient httpClient, IMemoryCache memoryCache, IOptions<ExchangeRateServiceSettings> exchangeRateServiceSettings) : base(memoryCache, httpClient, memoryCacheEntryOptions)
        {
            memoryCacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(exchangeRateServiceSettings.Value.ExchangeRateServiceCacheTime));
            httpClient.BaseAddress = new Uri(exchangeRateServiceSettings.Value.ExchangeRateServiceBaseUrl);
        }

        public async Task<ExchangeRates> GetExchangeRatesByDate(DateTime date)
        {
            var serviceResult = await CallGetEndpointCached<ExchangeRates>($"/webservices/ExchangeRates/ExchangeRates.asmx/getExchangeRatesByDate?Date={date.ToString("yyyy-MM-dd")}");

            return (serviceResult.StatusCode == System.Net.HttpStatusCode.OK ? serviceResult.Body : null);
        }
    }
}
