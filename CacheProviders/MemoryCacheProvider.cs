using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace CacheProviders
{
    public class MemoryCacheProvider : IMemoryCacheProvider
    {
        private readonly IMemoryCache _cache;

        public MemoryCacheProvider(IMemoryCache cache)
        {
            _cache = cache;
        }


        /// <summary>
        /// Try to pull the value from the cache, if the cache is empty then run the normal routine, the cache is update automatically with the value returned from the routine.
        /// </summary>
        /// <param name="key">Key for the cache</param>
        /// <param name="noCache">The delegate function that is run if the cache is empty</param>
        /// <param name="memoryCacheEntryOptions">Pass in standard memory cache options, Default: Sliding Expiration of 1 day.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>The value</returns>
        public async Task<T> TryGetValueAsync<T>(string key, NoCacheDelegateAsync<T> noCache, MemoryCacheEntryOptions memoryCacheEntryOptions = null)
        {
            if (_cache.TryGetValue(key, out T cacheEntry)) return cacheEntry;
            
            
            cacheEntry = await noCache();

            var cacheEntryOptions = memoryCacheEntryOptions ?? new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromDays(1));

            _cache.Set(key, cacheEntry, cacheEntryOptions);

            return cacheEntry;
        }      
        
        public T TryGetValue<T>(string key, NoCacheDelegate<T> noCache, MemoryCacheEntryOptions memoryCacheEntryOptions = null)
        {
            if (_cache.TryGetValue(key, out T cacheEntry)) return cacheEntry;
            
            
            cacheEntry = noCache();

            var cacheEntryOptions = memoryCacheEntryOptions ?? new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromDays(1));

            _cache.Set(key, cacheEntry, cacheEntryOptions);

            return cacheEntry;
        }
        
        

    }
}