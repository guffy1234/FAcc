namespace FuelAcc.Application.UseCases.Reports
{
    public sealed class ReportTransactionsDto
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

        public IReadOnlyCollection<Guid>? OrderId { get; set; }
        public IReadOnlyCollection<Guid>? SourceId { get; set; }
        public IReadOnlyCollection<Guid>? DestinationId { get; set; }
        public IReadOnlyCollection<Guid>? ProductId { get; set; }
    }

}