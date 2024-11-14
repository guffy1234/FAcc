using FuelAcc.Domain.Commons;
using FuelAcc.Domain.Entities.Other;

namespace FuelAcc.Application.UseCases.Commons.Events
{
    public record UpdateEvent<ENTITY>(DomainEvent<ENTITY> DomainEvent)
       : Event
       where ENTITY : class, IRootEntity;
}