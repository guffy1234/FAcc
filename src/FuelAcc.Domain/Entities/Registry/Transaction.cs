using FuelAcc.Domain.Commons;
using FuelAcc.Domain.Entities.Dictionaries;
using FuelAcc.Domain.Entities.Documents;

namespace FuelAcc.Domain.Entities.Registry
{
    public sealed class Transaction : IRootEntity
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }

        public Guid OrderId { get; set; }
        public OrderBase Order { get; set; }

        public Guid? SourceId { get; set; }
        public Rest? Source { get; set; }

        public Guid? DestinationId { get; set; }
        public Rest? Destination { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public decimal Quantity { get; set; }
    }
}