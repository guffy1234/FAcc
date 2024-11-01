using FuelAcc.Application.Interface.Persistence;
using FuelAcc.Domain.Entities.Registry;
using FuelAcc.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;


namespace FuelAcc.Persistence.Repositories
{
    internal class ReportsRepository : IReportsRepository
    {
        protected readonly AppDbContext _dbContext;

        public ReportsRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IAsyncEnumerable<Transaction> GetTransactions(DateTime? from, DateTime? to, 
            IEnumerable<Guid>? orderId, IEnumerable<Guid>? sourceId, IEnumerable<Guid>? destinationId, IEnumerable<Guid>? productId)
        {
            var queue = _dbContext.Set<Transaction>().AsQueryable();

            if(from.HasValue)
            {
                queue = queue.Where(q => q.Date >= from.Value);
            }
            if (to.HasValue)
            {
                queue = queue.Where(q => q.Date <= to.Value);
            }

            if (orderId !=null && orderId.Any())
            {
                if(orderId.Count() == 1)
                    queue = queue.Where(q => q.OrderId == orderId.First());
                else
                    queue = queue.Where(q => orderId.Contains(q.OrderId));
            }
            if (sourceId != null && sourceId.Any())
            {
                if (sourceId.Count() == 1)
                    queue = queue.Where(q => q.SourceId == sourceId.First());
                else
                    queue = queue.Where(q => sourceId.Contains(q.SourceId.Value));
            }
            if (destinationId != null && destinationId.Any())
            {
                if (destinationId.Count() == 1)
                    queue = queue.Where(q => q.DestinationId == destinationId.First());
                else
                    queue = queue.Where(q => destinationId.Contains(q.DestinationId.Value));
            }
            if (productId != null && productId.Any())
            {
                if (productId.Count() == 1)
                    queue = queue.Where(q => q.ProductId == productId.First());
                else
                    queue = queue.Where(q => productId.Contains(q.ProductId));
            }

            var items = queue.AsAsyncEnumerable();
            return items;
        }

        public IAsyncEnumerable<Rest> GetRests(
            IEnumerable<Guid>? storageId, IEnumerable<Guid>? productId)
        {
            var queue = _dbContext.Set<Rest>().AsQueryable();

            if (storageId != null && storageId.Any())
            {
                if (storageId.Count() == 1)
                    queue = queue.Where(q => q.StorageId == storageId.First());
                else
                    queue = queue.Where(q => storageId.Contains(q.StorageId));
            }
            if (productId != null && productId.Any())
            {
                if (productId.Count() == 1)
                    queue = queue.Where(q => q.ProductId == productId.First());
                else
                    queue = queue.Where(q => productId.Contains(q.ProductId));
            }

            var items = queue.AsAsyncEnumerable();
            return items;
        }
    }
}