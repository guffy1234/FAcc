using FuelAcc.Domain.Commons;
using FuelAcc.Domain.Entities.Other;

namespace FuelAcc.Application.Interface.Events;

public interface IEventService
{
    Task PublishEventAsync<ENTITY>(DomainEvent<ENTITY> domainEvent, CancellationToken cancellationToken)
        where ENTITY : class, IRootEntity;
}