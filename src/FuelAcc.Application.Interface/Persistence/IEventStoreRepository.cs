using FuelAcc.Domain.Commons;
using FuelAcc.Domain.Entities.Other;

namespace FuelAcc.Application.Interface.Exceptions;

public interface IEventStoreRepository
{
    Task InsertEventAsync<ENTITY>(DomainEvent<ENTITY> domainEvent, CancellationToken cancellationToken)
        where ENTITY : class, IRootEntity;
}