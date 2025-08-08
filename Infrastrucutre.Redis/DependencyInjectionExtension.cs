using CarStore.Domain.Cache;
using CarStore.Infrastrucutre.Redis.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace CarStore.Infrastrucutre.Redis
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddInfraRedisServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCacheServices(configuration);

            return services;
        }

        private static void AddCacheServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionStringRedis = configuration.GetConnectionString("ConnectionRedis")!;

            services.AddSingleton<IConnectionMultiplexer>(sp =>
                ConnectionMultiplexer.Connect(connectionStringRedis));

            services.AddScoped<ICacheService, RedisCacheService>();
        }
    }
}
