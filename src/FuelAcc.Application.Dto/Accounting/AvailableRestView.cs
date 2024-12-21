namespace FuelAcc.Application.Dto.Accounting
{
    public sealed class AvailableRestView
    {
        public Guid Id { get; set; }

        public Guid StorageId { get; set; }

        public Guid ProductId { get; set; }

        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
    }
}