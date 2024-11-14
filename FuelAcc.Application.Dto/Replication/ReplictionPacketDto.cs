namespace FuelAcc.Application.Dto.Replication
{
    public sealed class ReplictionPacketDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid? PreviousId { get; set; }

        public Guid BranchId { get; set; }
        public Guid SourceBranchId { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public ICollection<EventDto> Events { get; set; }
    }
}