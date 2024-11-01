namespace FuelAcc.Domain.Commons
{
    public interface IDocumentEntity : IRootEntity
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
    }
}