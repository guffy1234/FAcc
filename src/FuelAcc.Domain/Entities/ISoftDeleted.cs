namespace FuelAcc.Domain.Entities
{
    public interface ISoftDeleted
    {
        public bool IsDeleted { get; set; }
    }
}