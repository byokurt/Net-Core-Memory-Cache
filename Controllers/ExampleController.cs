using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Net_Core_Memory_Cache.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExampleController : ControllerBase
    {
        private readonly ILogger<ExampleController> _logger;
        private readonly IMemoryCache _memoryCache;

        public ExampleController(ILogger<ExampleController> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public string Get()
        {
            string cacheKey = "okurt";
            string response = string.Empty;

            _memoryCache.TryGetValue(cacheKey, out response);

            if (!string.IsNullOrWhiteSpace(response))
            {
                return response;
            }
            else
            {
                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(30),
                    Priority = CacheItemPriority.Normal
                };

                response = DateTime.Now.ToString();

                _memoryCache.Set(cacheKey, response, cacheOptions);

                return response;
            }
        }
    }
}