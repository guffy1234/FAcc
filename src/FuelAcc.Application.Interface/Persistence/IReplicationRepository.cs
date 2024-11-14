using FuelAcc.Domain.Entities.Other;

namespace FuelAcc.Application.Interface.Persistence;

public interface IReplicationRepository
{
    Task<ReplictionPacket?> GetLastAsync(Guid branchId, bool outbound, CancellationToken cancellationToken);
    Task<ReplictionPacket?> GetAsync(Guid Id, CancellationToken cancellationToken);
    Task<Guid> GetCurretBranchAsync(CancellationToken cancellationToken);
    Task InsertAsync(ReplictionPacket entity, CancellationToken cancellationToken);
}
