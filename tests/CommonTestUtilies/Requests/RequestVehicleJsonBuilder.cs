using Bogus;
using CarStore.Communication.Requests;

namespace CommonTestUtilies.Requests
{
    public class RequestVehicleJsonBuilder
    {
        public static RequestVehicleJson Build(string? brandId = null, string? typeId = null)
        {
            return new Faker<RequestVehicleJson>()
                .RuleFor(v => v.Model, f => f.Vehicle.Model())
                .RuleFor(v => v.Color, f => f.Commerce.Color())
                .RuleFor(v => v.YearFabrication, f => f.Date.Past(20).Year)
                .RuleFor(v => v.LicensePlate, f => f.Vehicle.Vin().Substring(0, 7).ToUpper())
                .RuleFor(v => v.EnginePower, f => $"{f.Random.Double(1.0, 3.0):0.0} CV")
                .RuleFor(v => v.Mileage, f => f.Random.Int(0, 200_000))
                .RuleFor(v => v.BrandId, _ => brandId ?? Guid.NewGuid().ToString())
                .RuleFor(v => v.TypeId, _ => typeId ?? Guid.NewGuid().ToString());
        }
    }
}
