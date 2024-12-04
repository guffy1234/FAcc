using FuelAcc.Application.Interface.Persistence;
using FuelAcc.Domain.Entities.Other;
using FuelAcc.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FuelAcc.Persistence.Repositories
{
    public class SettingsRepository : ISettingsRepository
    {
        protected readonly AppDbContext _dbContext;

        public SettingsRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Settings?> GetAsync(CancellationToken cancellationToken)
        {
            var res = await _dbContext.Settings.AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            return res;
        }

        public async Task UpsertAsync(Settings entity, CancellationToken cancellationToken)
        {
            var res = await _dbContext.Settings.AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            if (res is null)
                await _dbContext.Settings.AddAsync(entity, cancellationToken);
            else
                _dbContext.Settings.Update(entity);
        }
    }
}