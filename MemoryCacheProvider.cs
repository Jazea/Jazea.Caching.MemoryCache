using System;
using Microsoft.Extensions.Caching.Memory;
using CacheManager.Core;

namespace Jazea.Caching
{
    public class MemoryCacheProvider<TValue> : ICacheProvider<TValue>
    {
        private readonly IMemoryCache _cacheManager;

        public MemoryCacheProvider()
        {
            _cacheManager = new MemoryCache(new MemoryCacheOptions());
        }

        public bool Exists(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            TValue value = default(TValue);
            return _cacheManager.TryGetValue<TValue>(key, out value);
        }

        public TValue Get(string key)
        {
            if (Exists(key))
                return _cacheManager.Get<TValue>(key);

            return default(TValue);
        }

        public void Remove(string key)
        {
            if (Exists(key))
                _cacheManager.Remove(key);
        }

        public void Add(string key, TValue value, ExpirationMode expiration, TimeSpan timeout)
        {
            Remove(key);
            _cacheManager.Set(key, value, timeout);
        }
    }
}
