using FuelAcc.Application.Interface.Replication;
using FuelAcc.Domain.Commons;
using FuelAcc.Domain.Entities.Dictionaries;
using FuelAcc.Domain.Entities.Other;
using FuelAcc.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace FuelAcc.Persistence.Repositories
{
    internal class EventStoreRepository : IEventStoreRepository
    {
        protected readonly AppDbContext _dbContext;
        private Guid _currentBranchId;

        public EventStoreRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IAsyncEnumerable<PersistEvent> GetEventsAsync(Guid originBranchId, DateTime? from, DateTime to)
        {
            var query = _dbContext.Events
                .Where(p => p.BranchId == originBranchId).AsQueryable();

            if (from.HasValue)
            {
                query = query.Where(p => p.Date > from.Value).AsQueryable();
            }

            var result = query.Where(p => p.Date <= to)
                .OrderBy(p => p.Date)
                .AsAsyncEnumerable();

            return result;
        }

        public async Task InsertEventAsync(PersistEvent persistEvent, CancellationToken cancellationToken)
        {
            if (persistEvent.BranchId == Guid.Empty)
            {
                await ReadCurrentBranchAsync(cancellationToken);
                persistEvent.BranchId = _currentBranchId;
            }

            await _dbContext.AddAsync(persistEvent, cancellationToken);
        }

        private async Task ReadCurrentBranchAsync(CancellationToken cancellationToken)
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
                    throw new InvalidOperationException("There is no main Branch in Settings");
                }
            }
        }
    }
}