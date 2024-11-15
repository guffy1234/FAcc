namespace FuelAcc.Application.UseCases.Reports
{
    public sealed class ReportTransactionsDto
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

        public List<Guid>? OrderId { get; set; }
        public List<Guid>? SourceId { get; set; }
        public List<Guid>? DestinationId { get; set; }
        public List<Guid>? ProductId { get; set; }
    }

}