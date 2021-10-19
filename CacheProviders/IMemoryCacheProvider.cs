using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace CacheProviders
{
    public interface IMemoryCacheProvider
    {
        Task<T> TryGetValueAsync<T>(string key, NoCacheDelegateAsync<T> noCache,
            MemoryCacheEntryOptions memoryCacheEntryOptions = null);

        T TryGetValue<T>(string key, NoCacheDelegate<T> noCache,
            MemoryCacheEntryOptions memoryCacheEntryOptions = null);
    }
}