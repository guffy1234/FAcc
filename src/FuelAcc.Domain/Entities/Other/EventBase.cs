using FuelAcc.Domain.Entities.Dictionaries;
using FuelAcc.Domain.Enums;

namespace FuelAcc.Domain.Entities.Other
{
    public abstract class EventBase : IEvent
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }

        public Guid UserId { get; set; }

        public Guid BranchId { get; set; }
        public Branch Branch { get; set; }

        public ApplicationArea EventArea { get; set; } = ApplicationArea.Dictionary;
        public EventAction EventAction { get; set; } = EventAction.Insert;

        public Guid EntityId { get; set; }
    }
}