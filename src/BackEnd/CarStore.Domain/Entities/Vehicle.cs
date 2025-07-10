namespace CarStore.Domain.Entities
{
    public class Vehicle : EntityBase
    {
        public string Model { get; set; } = null!;
        public string? Color { get; set; }
        public int? YearFabrication { get; set; }
        public string? LicensePlate { get; set; }
        public string? EnginePower { get; set; }
        public int? Mileage { get; set; }
        public Guid? OwnerId { get; set; }
        public User? Owner { get; set; }
        public Guid? BrandId { get; set; }
        public Brand? Brand { get; set; }
        public Guid? TypeId { get; set; }
        public TypesVehicle? Type { get; set; }
    }
}
