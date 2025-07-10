namespace CarStore.Communication.Response
{
    public class ResponseVehicleJson
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool Active { get; set; }
        public string Model { get; set; }
        public string? Color { get; set; }
        public int? YearFabrication { get; set; }
        public string? LicensePlate { get; set; }
        public string? EnginePower { get; set; }
        public int? Mileage { get; set; }
        public ResponseUserBasicJson? Owner { get; set; }
        public ResponseBrandJson? Brand { get; set; }
        public ResponseVehicleTypeJson? Type { get; set; }
    }

}
