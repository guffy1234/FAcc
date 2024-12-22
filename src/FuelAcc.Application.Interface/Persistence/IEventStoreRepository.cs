using FuelAcc.Domain.Entities.Other;

namespace FuelAcc.Application.Interface.Replication;

public interface IEventStoreRepository
{
    Task InsertEventAsync(PersistEvent persistEvent, CancellationToken cancellationToken);

    IAsyncEnumerable<PersistEvent> GetEventsAsync(Guid originBranchId, DateTime? from, DateTime to);
}