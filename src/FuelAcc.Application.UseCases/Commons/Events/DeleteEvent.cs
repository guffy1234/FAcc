using FuelAcc.Domain.Commons;
using FuelAcc.Domain.Entities.Other;

namespace FuelAcc.Application.UseCases.Commons.Events
{
    public record DeleteEvent<ENTITY>(DomainEvent<ENTITY> DomainEvent) : Event
       where ENTITY : class, IRootEntity;
}