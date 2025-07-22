namespace CarStore.Domain.Dtos
{
    public class VehicleFilterDto
    {
        public string? Model { get; set; } = string.Empty;
        public string? OwnerName { get; set; } = string.Empty;
        public string? BrandName { get; set; } = string.Empty;
        public int? YearFabrication { get; set; }
    }
}
