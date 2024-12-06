using FuelAcc.Application.Dto.Querying;
using FuelAcc.Application.Dto.Replication;
using FuelAcc.Application.DtoCommon.Paging;

namespace FuelAcc.Application.Interface.Replication;

public interface IReplicationService
{
    Task<ReplictionPacketDto?> BuildOutboudPacketAsync(Guid targetBranchId, CancellationToken cancellationToken);
    Task ApplyInboundPacketAsync(ReplictionPacketDto packet, CancellationToken cancellationToken);
    Task<(string FileName, byte[] Data)?> BuildOutboudZipAsync(Guid targetBranchId, CancellationToken cancellationToken);
    Task ApplyInboundZipAsync(byte[] compressed, CancellationToken cancellationToken);
    Task<PagedResult<ReplictionPacketViewDto>> GetPagedHistoryAsync(ReplicationQueryDto querydto, CancellationToken cancellationToken);
}
