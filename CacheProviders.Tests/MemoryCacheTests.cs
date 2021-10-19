using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CacheProviders.Tests
{
    public class MemoryCacheTests
    {
        [Fact]
        public void MemoryCacheTryGetValue()
        {
            var serviceProvider = ServiceProviderHelper.ServiceProvider;
            var cacheProvider = serviceProvider.GetService<IMemoryCacheProvider>();

            var cache = serviceProvider.GetService<IMemoryCache>();
            const string returnValue = "Hello World!";
            const string key = "my_awesome_key";

            if (cacheProvider == null) return;
            var result = cacheProvider.TryGetValue(key, () => returnValue);
            Assert.Equal(returnValue, result);
            Assert.True(cache.TryGetValue(key, out string valueOfCache));
            Assert.Equal(returnValue, valueOfCache);
        }

        [Fact]
        public async Task MemoryCacheTryGetValueAsync()
        {


            var serviceProvider = ServiceProviderHelper.ServiceProvider;
  
            var cache = serviceProvider.GetService<IMemoryCache>();
            var cacheProvider = serviceProvider.GetService<IMemoryCacheProvider>();

            const string returnValue = "Hello World!";
            const string key = "my_awesome_key";
            
            Debug.Assert(cacheProvider != null, nameof(cacheProvider) + " != null");
            var result = await cacheProvider.TryGetValueAsync(key, () => Task.FromResult(returnValue));
            
            Assert.Equal(returnValue, result);

            Assert.True(cache.TryGetValue(key, out string valueOfCache));
            Assert.Equal(returnValue, valueOfCache);
            
        }
    }
}