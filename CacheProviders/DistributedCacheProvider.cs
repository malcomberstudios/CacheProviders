using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace CacheProviders
{
    public class DistributedCacheProvider : IDistributedCacheProvider
    {

      
        private readonly IDistributedCache _cache;

        public DistributedCacheProvider(IDistributedCache cache)
        {
            _cache = cache;
        }

        


        public async Task<T> TryGetValueAsync<T>(string key, NoCacheDelegateAsync<T> noCache,
            DistributedCacheEntryOptions distributedCacheEntryOptions = null)
        {
            var existingValue = await _cache.GetAsync(key);


            T returnValue;
            if (existingValue == null)
            {
                returnValue = await noCache();
                var bytes = Helpers.ObjectToByteArray(returnValue);
                var cacheOptions = distributedCacheEntryOptions ?? new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromDays(1));
                
                await _cache.SetAsync(key, bytes, cacheOptions);
            }
            else
            {
                returnValue = Helpers.ByteArrayToObject<T>(existingValue);
            }

            return returnValue;
        }

        public T TryGetValue<T>(string key, NoCacheDelegate<T> noCache, DistributedCacheEntryOptions distributedCacheEntryOptions = null)
        {
            var existingValue = _cache.Get(key);


            T returnValue;
            if (existingValue == null)
            {
                returnValue = noCache();
                var bytes = Helpers.ObjectToByteArray(returnValue);
                var cacheOptions = distributedCacheEntryOptions ?? new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromDays(1));
                
                _cache.Set(key, bytes, cacheOptions);
            }
            else
            {
                returnValue = Helpers.ByteArrayToObject<T>(existingValue);
            }

            return returnValue;
        }
    }
}