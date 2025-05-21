namespace CarStore.Domain.Entities
{
    public abstract class EntityBase
    {
        public Guid Id { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public bool Active { get; private set; }
        protected EntityBase()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.UtcNow;
            Active = true;
        }
    }

}
