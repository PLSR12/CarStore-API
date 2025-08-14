using CarStore.Domain.Cache;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace CarStore.Infrastrucutre.Redis.Service
{

    public class RedisCacheService : ICacheService
    {
        private readonly IDatabase _database;

        public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
        {
            _database = connectionMultiplexer.GetDatabase();
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var value = await _database.StringGetAsync(key);
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new Infrastructure.Redis.Service.JsonSerializationExtension.PrivateSetterContractResolver()
            };
            var data = value.IsNullOrEmpty ? default : JsonConvert.DeserializeObject<T>(value, settings);

            return data;
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            var json = JsonConvert.SerializeObject(value);
            await _database.StringSetAsync(key, json, expiry);
        }

        public async Task RemoveAsync(string key)
        {
            await _database.KeyDeleteAsync(key);
        }
    }
}
