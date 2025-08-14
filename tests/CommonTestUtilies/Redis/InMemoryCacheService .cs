using System.Collections.Concurrent;
using CarStore.Domain.Cache;

namespace CommonTestUtilies.Redis
{
    public class InMemoryCacheService : ICacheService
    {
        private readonly ConcurrentDictionary<string, object> _cache = new();

        public Task<T?> GetAsync<T>(string key)
        {
            if (_cache.TryGetValue(key, out var value))
            {
                return Task.FromResult((T?)value);
            }
            return Task.FromResult<T?>(default);
        }

        public Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            _cache[key] = value!;
            return Task.CompletedTask;
        }

        public Task RemoveAsync(string key)
        {
            _cache.TryRemove(key, out _);
            return Task.CompletedTask;
        }
    }
}
