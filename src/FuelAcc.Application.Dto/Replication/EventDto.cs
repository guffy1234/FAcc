using FuelAcc.Domain.Enums;

namespace FuelAcc.Application.Dto.Replication
{
    public class EventDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }

        public Guid UserId { get; set; }

        public Guid BranchId { get; set; }

        public ApplicationArea EventArea { get; set; } = ApplicationArea.Dictionary;
        public EventAction EventAction { get; set; } = EventAction.Insert;

        public Guid EntityId { get; set; }

        public string? ObjectClass { get; set; }
        public string? ObjectJson { get; set; }
    }
}