using FuelAcc.Domain.Commons;
using FuelAcc.Domain.Entities.Other;

namespace FuelAcc.Application.Interface.Events;

public interface IEventConverter
{
    object? ToMediatorEvent(PersistEvent persistEvent);

    PersistEvent ToPersistEvent<ENTITY>(DomainEvent<ENTITY> domainEvent)
        where ENTITY : class, IRootEntity;
}