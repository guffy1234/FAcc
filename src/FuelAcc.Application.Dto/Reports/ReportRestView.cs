namespace FuelAcc.Application.UseCases.Reports
{
    public sealed class ReportRestView
    {
        public Guid Id { get; set; }
        public Guid StorageId { get; set; }
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
    }
}