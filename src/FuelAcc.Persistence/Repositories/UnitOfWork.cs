using FuelAcc.Application.Interface.Persistence;
using FuelAcc.Persistence.Contexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace FuelAcc.Persistence.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken)
        {
            if (_transaction != null)
            {
                return;
            }
            _transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
            if (_transaction != null)
            {
                await _transaction.CommitAsync(cancellationToken);
                _transaction = null;
            }
        }
    }
}