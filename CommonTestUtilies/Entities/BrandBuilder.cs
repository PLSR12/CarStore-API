using Bogus;
using CarStore.Domain.Entities;

namespace CommonTestUtilies.Entities
{
    public class BrandBuilder
    {
        public static Brand Build()
        {
            return new Faker<Brand>()
            .RuleFor(b => b.Id, _ => Guid.NewGuid())
            .RuleFor(b => b.Name, f => f.Vehicle.Manufacturer())
            .RuleFor(b => b.Description, f => f.Vehicle.Manufacturer())
            .Generate();
        }
    }
}
