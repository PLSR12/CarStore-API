using System.Reflection;
using CarStore.Application.Services.Cryptography;
using CarStore.Domain.Repositories;
using CarStore.Domain.Repositories.User;
using CarStore.Domain.Security.Cryptography;
using CarStore.Infrastructure.DataAccess;
using CarStore.Infrastructure.DataAccess.Repositories;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarStore.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            var connectionString = configuration.ConnectionString();
            AddRepositories(services);
            AddPasswordEncrypter(services, configuration);

            if (configuration.IsUnitTestEnviroment())
            {
                return;
            }

            AddDbContext(services, connectionString);
            AddFluentMigrator(services, configuration);
        }

        private static void AddDbContext(IServiceCollection services, string? connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("connectionString can be defined");
            }
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 34));

            services.AddDbContext<CarStoreDbContext>(dbContextOptions =>
            {
                dbContextOptions.UseMySql(connectionString, serverVersion);
            });
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();

        }

        private static void AddFluentMigrator(
            IServiceCollection services,
            IConfiguration configuration
        )
        {
            services
                .AddFluentMigratorCore()
                .ConfigureRunner(options =>
                {
                    var connectionString = configuration.ConnectionString();

                    options
                        .AddMySql8()
                        .WithGlobalConnectionString(connectionString)
                        .ScanIn(Assembly.Load("CarStore.Infrastructure"))
                        .For.All();
                });
        }
        private static void AddPasswordEncrypter(IServiceCollection services, IConfiguration configuration)
        {
            var additionalKey = configuration.GetValue<string>("Settings:Password:AdditionalKey");

            services.AddScoped<IPasswordEncripter>(option => new Sha512Encripter(additionalKey!));
        }
    }
}