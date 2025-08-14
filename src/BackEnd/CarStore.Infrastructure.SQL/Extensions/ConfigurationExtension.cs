using Microsoft.Extensions.Configuration;

namespace CarStore.Infrastructure
{
    public static class ConfigurationExtension
    {
        public static bool IsUnitTestEnviroment(this IConfiguration configuration) => configuration.GetValue<bool>("InMemoryTest");
        public static string? ConnectionString(this IConfiguration configuration)
        {

            var connectionString = configuration.GetConnectionString("ConnectionMySql");

            return connectionString;
        }

    }
}
