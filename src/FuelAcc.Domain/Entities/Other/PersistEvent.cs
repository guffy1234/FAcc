using FuelAcc.Domain.Commons;

namespace FuelAcc.Domain.Entities.Other
{
    public sealed class PersistEvent : EventBase, IRootEntity, IEvent
    {
        public string ObjectClass { get; set; }
        public string? ObjectJson { get; set; }
    }
}