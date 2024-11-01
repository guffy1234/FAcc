using FuelAcc.Domain.Commons;
using FuelAcc.Domain.Entities.Dictionaries;

namespace FuelAcc.Domain.Entities.Other
{
    public sealed class ReplictionPacket : IRootEntity
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }

        public Guid BranchId { get; set; }
        public Branch Branch { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public bool Outbound { get; set; }
    }
}