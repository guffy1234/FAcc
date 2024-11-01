using FuelAcc.Domain.Entities.Dictionaries;

namespace FuelAcc.Domain.Entities.Other
{
    public sealed class Settings
    {
        public int Id { get; private set; }
        public Guid BranchId { get; set; }
        public Branch Branch { get; set; }
    }
}