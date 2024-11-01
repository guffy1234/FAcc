namespace FuelAcc.Domain.Commons
{
    public interface IDictionaryEntity : IRootEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}