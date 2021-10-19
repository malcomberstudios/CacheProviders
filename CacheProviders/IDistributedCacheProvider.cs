using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace CacheProviders
{
    public interface IDistributedCacheProvider
    {
        Task<T> TryGetValueAsync<T>(string key, NoCacheDelegateAsync<T> noCache,
            DistributedCacheEntryOptions distributedCacheEntryOptions = null);

        T TryGetValue<T>(string key, NoCacheDelegate<T> noCache,
            DistributedCacheEntryOptions distributedCacheEntryOptions = null);
    }
}