using FuelAcc.Domain.Entities.Dictionaries;
using FuelAcc.Domain.Enums;

namespace FuelAcc.Domain;

public interface IEvent
{
    Guid Id { get; set; }
    Guid UserId { get; set; }
    Branch Branch { get; set; }
    Guid BranchId { get; set; }
    DateTime Date { get; set; }
    Guid EntityId { get; set; }
    EventAction EventAction { get; set; }
    ApplicationArea EventArea { get; set; }
}