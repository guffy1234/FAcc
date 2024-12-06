using FuelAcc.Domain.Entities.Other;
using FuelAcc.Domain.Entities.ReportingModels;

namespace FuelAcc.Application.Interface.Persistence;

public interface IReplicationRepository
{
    Task<ReplictionPacket?> GetLastAsync(Guid branchId, bool outbound, CancellationToken cancellationToken);
    Task<ReplictionPacket?> GetAsync(Guid Id, CancellationToken cancellationToken);
    Task<Guid> GetCurretBranchAsync(CancellationToken cancellationToken);
    Task InsertAsync(ReplictionPacket entity, CancellationToken cancellationToken);
    Task<(int Total, IList<ReplictionPacketView> Items)> GetExtendedAsync(Func<IQueryable<ReplictionPacketView>, IQueryable<ReplictionPacketView>> filter, int page, int pageSize, CancellationToken cancellationToken);
}
