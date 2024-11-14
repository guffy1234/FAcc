

using FuelAcc.Application.Interface.Persistence;
using FuelAcc.Domain.Commons;
using FuelAcc.Domain.Entities.Dictionaries;
using FuelAcc.Domain.Entities.Other;
using FuelAcc.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace FuelAcc.Persistence.Repositories
{
    public class ReplicationRepository : IReplicationRepository
    {
        protected readonly AppDbContext _dbContext;
        private Guid _currentBranchId;

        public ReplicationRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        private async Task ReadCurrentBranch(CancellationToken cancellationToken)
        {
            if (_currentBranchId == Guid.Empty)
            {
                var settings = await _dbContext.Settings.FirstOrDefaultAsync(cancellationToken);
                if (settings != null)
                {
                    _currentBranchId = settings.BranchId;
                }
                else
                {
                    throw new InvalidOperationException("There is no current branch!");
                }
            }
        }

        public async Task<ReplictionPacket?> GetAsync(Guid Id, CancellationToken cancellationToken)
        {
            var result = await _dbContext.ReplictionPackets.FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
            return result;
        }

        public async Task<Guid> GetCurretBranchAsync(CancellationToken cancellationToken)
        {
            await ReadCurrentBranch(cancellationToken);
            return _currentBranchId;
        }

        public async Task<ReplictionPacket?> GetLastAsync(Guid branchId, bool outbound, CancellationToken cancellationToken)
        {
            var result = await _dbContext.ReplictionPackets
                .Where(p => p.BranchId==branchId && p.Outbound==outbound)
                .OrderByDescending(p => p.Date)
                .FirstOrDefaultAsync(cancellationToken);
            
            return result;
        }

        public async Task InsertAsync(ReplictionPacket entity, CancellationToken cancellationToken)
        {
            await _dbContext.ReplictionPackets.AddAsync(entity, cancellationToken);
        }
    }
}