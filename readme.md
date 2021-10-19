# Cache Providers

Helper functions for `IMemoryCache` & `IDistributedCache`, to reduce duplicate code. 

## Install

### Package Manager
`Install-Package CacheProviders -Version 1.0.0`

### .NET CLI
`dotnet add package CacheProviders --version 1.0.0`

## Introduction

The purpose of these functions is to validate there is a value within the cache, if not a delegate function is fired off which will automatically add the data to the cache.

All functions are built with a generic type, so complex objects can be used.

## Example

There is a service collection extension to add both Memory and Distributed providers

`services.AddCacheProviders()`

### Memory Cache

```c#
        private MemoryCacheProvider _memoryCacheProvider;

        public MemoryCacheTests(MemoryCacheProvider memoryCacheProvider)
        {
            _memoryCacheProvider = memoryCacheProvider;
        }

        public object BasicUsage()
        {
            return _memoryCacheProvider.TryGetValue("My Key", () =>
            {
                // Get Actual Value if key doesn't exist.
            });
        }
        
        // Example of getting a string value
        public string GetName()
        {
            return _memoryCacheProvider.TryGetValue("_MyName", () => "Engelbert Humperdinck");
        }
        
        // Each function has an async equivilent
        public Task<string> GetNameAsync()
        {
            return _memoryCacheProvider.TryGetValueAsync("_MyName", () => Task.FromResult("Engelbert Humperdinck"));
        }
        
        // You can pass in the cache options
        public string GetNameWithOptions()
        {
            var options = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(15));
            
            return _memoryCacheProvider.TryGetValue("_MyName", () => "Engelbert Humperdinck", options);
        }
```

### Distributed Cache

```c#
        private DistributedCacheProvider _distributedCacheProvider;

        public MemoryCacheTests(DistributedCacheProvider distributedCacheProvider)
        {
            _distributedCacheProvider = distributedCacheProvider;
        }
        
        public object BasicUsage()
        {
            return _distributedCacheProvider.TryGetValue("My Key", () =>
            {
                // Get Actual Value if key doesn't exist.
            });
        }
        
        // Example of getting a string value
        public string GetName()
        {
            return _distributedCacheProvider.TryGetValue("_MyName", () => "Engelbert Humperdinck");
        }
        
        // Each function has an async equivilent
        public Task<string> GetNameAsync()
        {
            return _distributedCacheProvider.TryGetValueAsync("_MyName", () => Task.FromResult("Engelbert Humperdinck"));
        }

        // You can pass in the cache options
        public string GetNameWithOptions()
        {
            var options = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(15));
            
            return _distributedCacheProvider.TryGetValue("_MyName", () => "Engelbert Humperdinck", options);
        }
```

## Todo

[ ] Create generic options for both types caching.