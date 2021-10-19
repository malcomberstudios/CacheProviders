using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;

namespace CacheProviders
{
    public static class Helpers
    {
        public static byte[] ObjectToByteArray(object obj)
        {
            return JsonSerializer.SerializeToUtf8Bytes(obj);
        }

        public static T ByteArrayToObject
            <T>(byte[] obj)
        {
            return JsonSerializer.Deserialize<T>(obj);
        }

        public static IServiceCollection AddCacheProviders(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<MemoryCacheProvider>();
            serviceCollection.AddTransient<DistributedCacheProvider>();
            return serviceCollection;
        }
    }
}