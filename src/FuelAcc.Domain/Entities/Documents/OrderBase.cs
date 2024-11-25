using FuelAcc.Domain.Commons;

namespace FuelAcc.Domain.Entities.Documents
{
    public abstract class OrderBase : ChangesInfo, IDocumentEntity
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string Number { get; set; }
        public string Description { get; set; }
        public decimal Total { get; set; }
        public List<OrderLine> Lines { get; set; }
    }
}