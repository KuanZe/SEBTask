using Microsoft.Extensions.Caching.Memory;
using SEBTask.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SEBTask.Services.Base
{
    public abstract class ApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _memoryCache;
        private readonly MemoryCacheEntryOptions _memoryCacheEntryOptions;

        public ApiClient(IMemoryCache memoryCache, HttpClient httpClient, MemoryCacheEntryOptions memoryCacheEntryOptions)
        {
            _memoryCache = memoryCache;
            _httpClient = httpClient;
            _memoryCacheEntryOptions = memoryCacheEntryOptions;
        }

        public async Task<ApiClientResult<T>> CallGetEndpoint<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);
            var responseStr = await response.Content.ReadAsStringAsync();
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StringReader stringReader = new StringReader(responseStr);
            T model;
            try
            {
                model = (T)serializer.Deserialize(stringReader);
            }
            catch (Exception e)
            {
                model = default(T);
            }
            return new ApiClientResult<T>(response.StatusCode, model);
        }

        public async Task<ApiClientResult<T>> CallGetEndpointCached<T>(string url)
        {
            ApiClientResult<T> result;
            if (!_memoryCache.TryGetValue<ApiClientResult<T>>("cache_" + _httpClient.BaseAddress + url, out result))
            {
                result = await CallGetEndpoint<T>(url);
                _memoryCache.Set<ApiClientResult<T>>("cache_" + _httpClient.BaseAddress + url, result, _memoryCacheEntryOptions);
            }
            return result;
        }
    }
}
