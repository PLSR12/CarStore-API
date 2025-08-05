namespace CarStore.Communication.Requests
{
    public class RequestVehicleJson
    {
        public string Model { get; set; }
        public string? Color { get; set; }
        public int? YearFabrication { get; set; }
        public string? LicensePlate { get; set; }
        public string? EnginePower { get; set; }
        public int? Mileage { get; set; }
        public string BrandId { get; set; }
        public string TypeId { get; set; }
    }
}

