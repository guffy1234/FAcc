using FuelAcc.Domain.Entities.Dictionaries;
using FuelAcc.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FuelAcc.Application.Dto.Replication
{
    public class EventDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid BranchId { get; set; }

        [Required]
        public ApplicationArea EventArea { get; set; } = ApplicationArea.Dictionary;
        [Required]
        public EventAction EventAction { get; set; } = EventAction.Insert;

        [Required]
        public Guid EntityId { get; set; }

        public string? ObjectClass { get; set; }
        public string? ObjectJson { get; set; }
    }
}