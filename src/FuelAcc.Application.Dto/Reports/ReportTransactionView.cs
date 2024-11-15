namespace FuelAcc.Application.UseCases.Reports
{
    public sealed class ReportTransactionView
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid OrderId { get; set; }
        public Guid? SourceId { get; set; }
        public Guid? DestinationId { get; set; }
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
    }

}