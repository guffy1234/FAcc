namespace FuelAcc.Domain.Entities.ReportingModels
{
    public sealed class TransactionReport
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }

        public Guid OrderId { get; set; }
        public string OrderNumber { get; set; }

        public Guid? SrcRestId { get; set; }
        public Guid? SrcStorageId { get; set; }
        public string? SrcStorageName { get; set; }

        public Guid? DstRestId { get; set; }
        public Guid? DstStorageId { get; set; }
        public string? DstStorageName { get; set; }

        public Guid ProductId { get; set; }
        public string ProductName { get; set; }

        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
    }
}