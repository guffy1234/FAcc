using FuelAcc.Domain.Commons;

namespace FuelAcc.Domain.Entities.Other
{
    public sealed class DomainEvent<ENTITY> : EventBase where ENTITY : class, IRootEntity
    {
        public ENTITY Entity { get; set; }
    }
}