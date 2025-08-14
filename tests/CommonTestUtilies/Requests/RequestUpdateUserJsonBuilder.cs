using Bogus;
using CarStore.Communication.Requests;

namespace CommonTestUtilies.Requests
{
    public class RequestUpdateUserJsonBuilder
    {
        public static RequestUpdateUserJson Build()
        {
            return new Faker<RequestUpdateUserJson>()
                  .RuleFor(user => user.Name, (f) => f.Person.FirstName);

        }
    }
}
