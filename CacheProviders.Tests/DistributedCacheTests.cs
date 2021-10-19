using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CacheProviders.Tests
{
    public class DistributedCacheTests
    {
        [Fact]
        public void DistributedCacheTryGetValue()
        {
            var serviceProvider = ServiceProviderHelper.ServiceProvider;
            var cacheProvider = serviceProvider.GetService<IDistributedCacheProvider>();

            var cache = serviceProvider.GetService<IDistributedCache>();
            const string returnValue = "Hello World!";
            const string key = "my_awesome_key";

            Debug.Assert(cacheProvider != null, nameof(cacheProvider) + " != null");
            var result = cacheProvider.TryGetValue(key, () => returnValue);
            
            Assert.Equal(returnValue, result);

            Debug.Assert(cache != null, nameof(cache) + " != null");
            Assert.Equal(JsonSerializer.SerializeToUtf8Bytes(returnValue), cache.Get(key));
            
        }
        
        [Fact]
        public async Task DistributedCacheTryGetValueAsync()
        {
            var serviceProvider = ServiceProviderHelper.ServiceProvider;
            var cacheProvider = serviceProvider.GetService<IDistributedCacheProvider>();

            var cache = serviceProvider.GetService<IDistributedCache>();
            const string returnValue = "Hello World!";
            const string key = "my_awesome_key";

            Debug.Assert(cacheProvider != null, nameof(cacheProvider) + " != null");
            var result = await cacheProvider.TryGetValueAsync(key, () => Task.FromResult(returnValue));
            
            Assert.Equal(returnValue, result);

            Debug.Assert(cache != null, nameof(cache) + " != null");
            Assert.Equal(JsonSerializer.SerializeToUtf8Bytes(returnValue), await cache.GetAsync(key));
            
        }
    }
}