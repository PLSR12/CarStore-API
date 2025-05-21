namespace CarStore.Domain.Entities
{
    public class User : EntityBase
    {

        public string Name { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string OwnerCar { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;

    }
}
