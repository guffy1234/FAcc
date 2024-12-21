using FuelAcc.Application.Interface.Persistence;
using FuelAcc.Domain.Entities.Registry;
using FuelAcc.Domain.Entities.ReportingModels;
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

        public IAsyncEnumerable<TransactionReport> GetTransactions(DateTime? from, DateTime? to,
            IEnumerable<Guid>? orderId, IEnumerable<Guid>? sourceId, IEnumerable<Guid>? destinationId, IEnumerable<Guid>? productId)
        {
            //var queue = _dbContext.Set<Transaction>().AsQueryable();
            var queue = from trans in _dbContext.Transactions
                        join product in _dbContext.Products
                        on trans.ProductId equals product.Id
                        join order in _dbContext.Orders
                        on trans.OrderId equals order.Id
                        join srest in _dbContext.Rests 
                        on trans.SourceId equals srest.Id into srest_trans
                        from srest_left in srest_trans.DefaultIfEmpty()
                        join sstorage in _dbContext.Storages
                        on srest_left.StorageId equals sstorage.Id into srest_storage
                        from sstorage_left in srest_storage.DefaultIfEmpty()
                        join drest in _dbContext.Rests
                        on trans.DestinationId equals drest.Id into drest_trans 
                        from drest_left in drest_trans.DefaultIfEmpty()
                        join dstorage in _dbContext.Storages
                        on drest_left.StorageId equals dstorage.Id into drest_storage
                        from dstorage_left in drest_storage.DefaultIfEmpty()
                        select new TransactionReport()
                        {
                            Id = trans.Id,
                            Date = trans.Date,
                            OrderId = trans.OrderId,
                            OrderNumber = order.Number,
                            ProductId = product.Id,
                            ProductName = product.Name,
                            SrcRestId = srest_left.Id,
                            SrcStorageId = sstorage_left.Id,
                            SrcStorageName = sstorage_left.Name,
                            DstRestId = drest_left.Id,
                            DstStorageId = dstorage_left.Id,
                            DstStorageName = dstorage_left.Name,
                            Quantity = trans.Quantity,
                            Price = trans.Price,
                        };

            if (from.HasValue)
            {
                queue = queue.Where(q => q.Date >= from.Value);
            }
            if (to.HasValue)
            {
                queue = queue.Where(q => q.Date <= to.Value);
            }

            if (orderId != null && orderId.Any())
            {
                if (orderId.Count() == 1)
                    queue = queue.Where(q => q.OrderId == orderId.First());
                else
                    queue = queue.Where(q => orderId.Contains(q.OrderId));
            }
            if (sourceId != null && sourceId.Any())
            {
                if (sourceId.Count() == 1)
                    queue = queue.Where(q => q.SrcStorageId == sourceId.First());
                else
                    queue = queue.Where(q => sourceId.Contains(q.SrcStorageId.Value));
            }
            if (destinationId != null && destinationId.Any())
            {
                if (destinationId.Count() == 1)
                    queue = queue.Where(q => q.DstStorageId == destinationId.First());
                else
                    queue = queue.Where(q => destinationId.Contains(q.DstStorageId.Value));
            }
            if (productId != null && productId.Any())
            {
                if (productId.Count() == 1)
                    queue = queue.Where(q => q.ProductId == productId.First());
                else
                    queue = queue.Where(q => productId.Contains(q.ProductId));
            }

            // default sorting
            // todo: implement custom sorting depend on passed DTO
            queue = queue.OrderByDescending(t => t.Date);

            var items = queue.AsAsyncEnumerable();
            return items;
        }

        public IAsyncEnumerable<RestReport> GetRests(bool nonEmptyOnly,
            IEnumerable<Guid>? storageId, IEnumerable<Guid>? productId)
        {
            //var queue = _dbContext.Set<Rest>().AsQueryable();
            var queue = from rest in _dbContext.Rests
                        join storage in _dbContext.Storages
                        on rest.StorageId equals storage.Id 
                        join product in _dbContext.Products
                        on rest.ProductId equals product.Id 
                        join branch in _dbContext.Branches
                        on storage.BranchId equals branch.Id 
                        select new RestReport()
                        {
                            Id = rest.Id,
                            ProductId = product.Id,
                            ProductName = product.Name,
                            StorageId = storage.Id,
                            StorageName = storage.Name,
                            BranchId = branch.Id,
                            BranchName = branch.Name,
                            Quantity = rest.Quantity,
                            Price = rest.Price,
                        };

            if (nonEmptyOnly)
            {
                queue = queue.Where(r => r.Quantity > 0);
            }

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

            // default sorting
            // todo: implement custom sorting depend on passed DTO
            queue = queue
                .OrderBy(t => t.BranchName)
                .OrderBy(t => t.StorageName)
                .OrderBy(t => t.ProductName);

            var items = queue.AsAsyncEnumerable();
            return items;
        }
    }
}