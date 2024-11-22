using FuelAcc.Application.Interface.Exceptions;
using FuelAcc.Application.Interface.Persistence;
using FuelAcc.Domain.Commons;
using FuelAcc.Domain.Entities;
using FuelAcc.Persistence.Contexts;
using MediatR;
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

        public async Task<(int Total, IList<T> Items)> GetExtendedAsync(Func<IQueryable<T>, IQueryable<T>> filter, int page, int pageSize, bool asNoTracked, CancellationToken cancellationToken)
        {
            var query = _dbContext.Set<T>().Where(e => !e.IsDeleted).AsQueryable();
            if (asNoTracked)
            {
                query = query.AsNoTracking();
            }
            if(filter != null)
            {
                query = filter(query);
            }

            if (pageSize > 0)
            {
                var count = await query.CountAsync(cancellationToken);
                var skip = (page - 1) * pageSize;

                query = query.Skip(skip).Take(pageSize);

                var items = await query.ToListAsync(cancellationToken);

                return (count, items);
            } else
            {
                var items = await query.ToListAsync(cancellationToken);

                return (items.Count(), items);
            }


        }

        public IAsyncEnumerable<T> GetAllAsync(bool asNoTracked = true)
        {
            var query = _dbContext.Set<T>().Where(e => !e.IsDeleted);
            if (asNoTracked)
            {
                query = query.AsNoTracking();
            }
            var items = query.AsAsyncEnumerable();
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