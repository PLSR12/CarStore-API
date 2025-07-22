using Bogus;
using CarStore.Domain.Entities;

namespace CommonTestUtilies.Entities
{
    public class TypesVehicleBuilder
    {
        public static TypesVehicle Build()
        {
            return new Faker<TypesVehicle>()
                .RuleFor(t => t.Id, _ => Guid.NewGuid())
                .RuleFor(t => t.Description, f => f.Random.Word())
                .Generate();
        }
    }
}
