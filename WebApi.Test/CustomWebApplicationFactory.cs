using CarStore.Infrastructure.DataAccess;
using CommonTestUtilies.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Test
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private CarStore.Domain.Entities.User _user = default!;
        private string _password = string.Empty;
        private string _name = string.Empty;


        public string GetEmail() => _user.Email;
        public string GetPassword() => _password;
        public string GetName() => _name;
        public Guid GetUserIdentifier() => _user.Id;



        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test").ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<CarStoreDbContext>));

                if (descriptor is not null)
                {
                    services.Remove(descriptor);
                }

                var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                services.AddDbContext<CarStoreDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                    options.UseInternalServiceProvider(provider);
                });

                using var scope = services.BuildServiceProvider().CreateScope();

                var dbContext = scope.ServiceProvider.GetRequiredService<CarStoreDbContext>();
                dbContext.Database.EnsureDeleted();

                StartDataBase(dbContext);
            });
        }

        private void StartDataBase(CarStoreDbContext dbContext)
        {
            (_user, _password) = UserBuilder.Build();

            _name = _user.Name;

            dbContext.Users.Add(_user);

            dbContext.SaveChanges();
        }

    }
}
