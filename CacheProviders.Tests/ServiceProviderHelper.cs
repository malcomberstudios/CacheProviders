using System;
using Microsoft.Extensions.DependencyInjection;



namespace CacheProviders.Tests
{
    public static class ServiceProviderHelper
    {
        public static IServiceProvider ServiceProvider
        {
            get
            {
                var services = new ServiceCollection();
                services.AddMemoryCache();
                services.AddDistributedMemoryCache();
                services.AddTransient<IMemoryCacheProvider, MemoryCacheProvider>();
                services.AddTransient<IDistributedCacheProvider, DistributedCacheProvider>();
                var serviceProvider = services.BuildServiceProvider();
         
                return serviceProvider;
            }
        }
    }
}