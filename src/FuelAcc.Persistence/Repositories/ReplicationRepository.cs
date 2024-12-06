using FuelAcc.Application.Interface.Persistence;
using FuelAcc.Domain.Entities.Other;
using FuelAcc.Domain.Entities.ReportingModels;
using FuelAcc.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

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
                .Where(p => p.BranchId == branchId && p.Outbound == outbound)
                .OrderByDescending(p => p.Date)
                .FirstOrDefaultAsync(cancellationToken);

            return result;
        }

        public async Task InsertAsync(ReplictionPacket entity, CancellationToken cancellationToken)
        {
            await _dbContext.ReplictionPackets.AddAsync(entity, cancellationToken);
        }

        public async Task<(int Total, IList<ReplictionPacketView> Items)> GetExtendedAsync(Func<IQueryable<ReplictionPacketView>, IQueryable<ReplictionPacketView>> filter, int page, int pageSize, CancellationToken cancellationToken)
        {
            var query = from replication in _dbContext.ReplictionPackets
                        join branch in _dbContext.Branches
                        on replication.BranchId equals branch.Id
                        select new ReplictionPacketView()
                        {
                            Id = replication.Id,
                            Date = replication.Date,
                            PreviousId = replication.PreviousId,
                            FromDate = replication.FromDate,
                            ToDate = replication.ToDate,
                            Outbound = replication.Outbound,
                            BranchId = branch.Id,
                            BranchName = branch.Name,
                        };

            query = query.AsNoTracking().AsQueryable();
            if (filter != null)
            {
                query = filter(query);
            }
            query = query.OrderByDescending(r => r.Date);
            if (pageSize > 0)
            {
                var count = await query.CountAsync(cancellationToken);
                var skip = (page - 1) * pageSize;

                query = query.Skip(skip).Take(pageSize);

                var items = await query.ToListAsync(cancellationToken);

                return (count, items);
            }
            else
            {
                var items = await query.ToListAsync(cancellationToken);

                return (items.Count(), items);
            }
        }
    }
}