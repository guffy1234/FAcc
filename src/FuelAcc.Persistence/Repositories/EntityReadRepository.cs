using FuelAcc.Application.Interface.Exceptions;
using FuelAcc.Application.Interface.Persistence;
using FuelAcc.Domain.Commons;
using FuelAcc.Domain.Entities;
using FuelAcc.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FuelAcc.Persistence.Repositories
{
    internal class EntityReadRepository<T> : IEntityReadRepository<T> where T : class, IRootEntity, ISoftDeleted
    {
        protected readonly AppDbContext _dbContext;

        public EntityReadRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IAsyncEnumerable<T> GetAllAsync()
        {
            var items = _dbContext.Set<T>().Where(e => !e.IsDeleted).AsAsyncEnumerable();
            return items;
        }

        public async Task<T> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var fetched = await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (fetched == null || fetched.IsDeleted)
            {
                throw new NotFoundException();
            }
            return fetched;
        }
    }
}