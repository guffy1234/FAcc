using FuelAcc.Domain.Commons;
using FuelAcc.Domain.Entities.Other;

namespace FuelAcc.Application.UseCases.Commons.Events
{

    public record CreateEvent<ENTITY>(DomainEvent<ENTITY> DomainEvent, bool IsInRepliactionContext = false) 
        : EventBase(IsInRepliactionContext)
        where ENTITY : class, IRootEntity;
}