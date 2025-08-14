using Bogus;
using CarStore.Domain.Entities;

namespace CommonTestUtilies.Entities
{
    public class VehicleBuilder
    {
        public static IList<Vehicle> Collection(
             User owner,
             uint count = 4,
             Brand? brand = null,
             string? model = null,
             int? yearFabrication = null)
        {
            if (count == 0)
                count = 1;

            var list = new List<Vehicle>();
            for (int i = 0; i < count; i++)
            {
                var vehicle = Build(owner, brand, null, model, yearFabrication);
                list.Add(vehicle);
            }
            return list;
        }

        public static Vehicle Build(
           User? owner = null,
           Brand? brand = null,
           TypesVehicle? type = null,
           string? model = null,
           int? yearFabrication = null)
        {
            owner ??= UserBuilder.Build().user;
            brand ??= BrandBuilder.Build();
            type ??= TypesVehicleBuilder.Build();

            var faker = new Faker("pt_BR");

            return new Faker<Vehicle>()
                .RuleFor(v => v.Id, _ => Guid.NewGuid())
                .RuleFor(v => v.Model, _ => model ?? faker.Vehicle.Model())
                .RuleFor(v => v.Color, f => f.Commerce.Color())
                .RuleFor(v => v.YearFabrication, _ => yearFabrication ?? faker.Date.Past(20).Year)
                .RuleFor(v => v.LicensePlate, f => f.Vehicle.Vin().Substring(0, 7).ToUpper())
                .RuleFor(v => v.EnginePower, f => $"{f.Random.Double(1.0, 3.0):0.0} CV")
                .RuleFor(v => v.Mileage, f => f.Random.Int(0, 200_000))
                .RuleFor(v => v.OwnerId, _ => owner.Id)
                .RuleFor(v => v.Owner, _ => owner)
                .RuleFor(v => v.BrandId, _ => brand.Id)
                .RuleFor(v => v.Brand, _ => brand)
                .RuleFor(v => v.TypeId, _ => type.Id)
                .RuleFor(v => v.Type, _ => type)
                .Generate();
        }

    }
}
