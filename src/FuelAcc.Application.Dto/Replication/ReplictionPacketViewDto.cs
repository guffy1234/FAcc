namespace FuelAcc.Application.Dto.Replication
{
    public sealed class ReplictionPacketViewDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid? PreviousId { get; set; }

        public Guid BranchId { get; set; }
        public string BranchName { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public bool Outbound { get; set; }
    }
}