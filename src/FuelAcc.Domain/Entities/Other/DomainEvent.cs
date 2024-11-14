using FuelAcc.Domain.Commons;

namespace FuelAcc.Domain.Entities.Other
{
    public abstract class DomainEventBase : EventBase, IRootEntity
    {
        public abstract object EntityObject { get; set; }
    }

    public sealed class DomainEvent<ENTITY> : DomainEventBase where ENTITY : class
    {
        public ENTITY Entity { get; set; }
        public override object EntityObject { 
            get => Entity; 
            set => Entity = value as ENTITY; }
    }
}