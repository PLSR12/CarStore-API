using Bogus;
using CarStore.Domain.Entities;
using CommonTestUtilies.Cryptography;

namespace CommonTestUtilies.Entities
{
    public class UserBuilder
    {
        public static (User user, string password) Build()
        {
            var passwordEncripter = PasswordEncripterBuilder.Build();

            var password = new Faker().Internet.Password();

            var user = new Faker<User>()
                     .RuleFor(user => user.Id, () => Guid.Parse("d45c65f8-19b0-41a1-922b-30fd6df93ef7"))
                     .RuleFor(user => user.Name, (f) => f.Person.FirstName)
                     .RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Name))
                     .RuleFor(user => user.Password, (f) => passwordEncripter.Encrypt(password));

            return (user, password);
        }
    }
}
