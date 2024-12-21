using FuelAcc.Application.Interface.Accounting;
using FuelAcc.Domain.Entities.Registry;
using FuelAcc.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FuelAcc.Persistence.Repositories
{
    internal class TransactionsRepository : ITransactionsRepository
    {
        protected readonly AppDbContext _dbContext;

        public TransactionsRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync(Guid documentId, CancellationToken cancellationToken)
        {
            var items = await _dbContext.Transactions
                .Where(e => e.OrderId == documentId)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            return items;
        }

        public async Task<IEnumerable<Rest>> GetNonEmptyRestsAsync(Guid storageId, Guid productId, CancellationToken cancellationToken)
        {
            var rests = await _dbContext.Rests
                .Where(e => e.StorageId == storageId && e.ProductId == productId && e.Quantity > 0)
                .ToListAsync(cancellationToken);
            return rests;
        }

        public async Task<Rest> GetRestAsync(Guid storageId, Guid productId, decimal price, CancellationToken cancellationToken)
        {
            var rest = await _dbContext.Rests
                .FirstOrDefaultAsync(e => e.StorageId == storageId && e.ProductId == productId && e.Price == price, cancellationToken);
            return rest;
        }

        public async Task<Rest> GetRestByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var rest = await _dbContext.Rests
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
            return rest;
        }

        public async Task DeleteAsync(IEnumerable<Transaction> transactions, CancellationToken cancellationToken)
        {
            _dbContext.Transactions.RemoveRange(transactions);
        }

        public async Task InsertAsync(IEnumerable<Transaction> transactions, CancellationToken cancellationToken)
        {
            await _dbContext.Transactions.AddRangeAsync(transactions, cancellationToken);
        }

        public async Task InsertRestAsync(Rest rest, CancellationToken cancellationToken)
        {
            await _dbContext.Rests.AddAsync(rest, cancellationToken);
        }

        public async Task UpdateAsync(IEnumerable<Transaction> transactions, CancellationToken cancellationToken)
        {
            _dbContext.Transactions.UpdateRange(transactions);
        }
    }
}