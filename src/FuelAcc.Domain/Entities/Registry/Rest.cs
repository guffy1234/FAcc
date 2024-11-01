using FuelAcc.Domain.Commons;
using FuelAcc.Domain.Entities.Dictionaries;

namespace FuelAcc.Domain.Entities.Registry
{
    public sealed class Rest : IRootEntity
    {
        public Guid Id { get; set; }

        public Guid StorageId { get; set; }
        public Storage Storage { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public decimal Quantity { get; set; }

        public ICollection<Transaction> InTransactions { get; set; }
        public ICollection<Transaction> OutTransactions { get; set; }
    }
}