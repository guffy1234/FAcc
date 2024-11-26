using FuelAcc.Application.Interface.Exceptions;
using FuelAcc.Application.Interface.Persistence;
using FuelAcc.Domain.Commons;
using FuelAcc.Domain.Entities;
using FuelAcc.Domain.Entities.Documents;
using FuelAcc.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FuelAcc.Persistence.Repositories
{
    internal class EntityWriteRepository<T> : IEntityWriteRepository<T> where T : class, IRootEntity, ISoftDeleted
    {
        protected readonly AppDbContext _dbContext;

        public EntityWriteRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var fetched = await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (fetched == null || fetched.IsDeleted)
            {
                throw new NotFoundException();
            }
            fetched.IsDeleted = true;
            _dbContext.Set<T>().Update(fetched);
        }

        public async Task InsertAsync(T entity, CancellationToken cancellationToken)
        {
            await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            var query = typeof(T).IsAssignableTo(typeof(OrderBase)) ?
                _dbContext.Set<T>().Include(nameof(OrderBase.Lines)).AsQueryable() :
                _dbContext.Set<T>().AsQueryable();

            var fetched = await query.AsNoTracking().FirstOrDefaultAsync(x => x.Id == entity.Id, cancellationToken);
            if (fetched == null || fetched.IsDeleted)
            {
                throw new NotFoundException();
            }
            // workarond for remove disappeared order lines
            if(entity is OrderBase eob && fetched is OrderBase fob)
            {
                var new_ids = eob.Lines.Select(x => x.Id).ToList(); 
                var removed = fob.Lines.Where(f =>  !new_ids.Contains(f.Id)).ToList();
                _dbContext.OrderLines.RemoveRange(removed);
            }
            _dbContext.Set<T>().Update(entity);
        }
    }
}