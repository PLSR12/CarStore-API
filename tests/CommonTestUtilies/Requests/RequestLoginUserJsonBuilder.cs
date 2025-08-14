using Bogus;
using CarStore.Communication.Requests;

namespace CommonTestUtilies.Requests
{
    public class RequestLoginUserJsonBuilder
    {
        public static RequestLoginUserJson Build(int passwordLength = 10)
        {

            return new Faker<RequestLoginUserJson>()
                  .RuleFor(user => user.Email, (f, user) => f.Internet.Email())
                  .RuleFor(user => user.Password, (f) => f.Internet.Password(passwordLength));
        }
    }
}
