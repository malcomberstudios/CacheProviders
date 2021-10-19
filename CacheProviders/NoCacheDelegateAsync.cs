using System.Threading.Tasks;

namespace CacheProviders
{
    public delegate Task<T> NoCacheDelegateAsync<T>();
}