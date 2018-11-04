using Microsoft.Extensions.Caching.Memory;
using System;

namespace AspNetCore.Security.Jwt.Microservice.Repository.Providers
{
    /// <summary>
    /// Class CacheProvider
    /// </summary>
    public class CacheProvider : ICacheProvider
    {
        IMemoryCache _cache;
        int _cacheDurationInHours;        

        public CacheProvider(IMemoryCache memoryCache, int cacheDurationInHours)
        {
            _cache = memoryCache;
            _cacheDurationInHours = cacheDurationInHours;
        }

        public T GetCacheEntry<T>(string key, Func<T> get)
            where T : class
        {
            T cacheEntry = null;

            if (!_cache.TryGetValue(key, out cacheEntry))
            {
                // Key not in cache, so get data.
                cacheEntry = get();

                if (cacheEntry != null)
                {
                    // Set cache options.
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        // Keep in cache for this time, reset time if accessed.
                        .SetSlidingExpiration(TimeSpan.FromHours(_cacheDurationInHours));

                    // Save data in cache.
                    _cache.Set(key, cacheEntry, cacheEntryOptions);
                }                
            }

            return cacheEntry;            
        }
    }
}
