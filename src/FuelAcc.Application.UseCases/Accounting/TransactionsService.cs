using FuelAcc.Application.Interface.Accounting;
using FuelAcc.Application.Interface.Exceptions;
using FuelAcc.Domain.Entities.Documents;
using FuelAcc.Domain.Entities.Registry;
using Transaction = FuelAcc.Domain.Entities.Registry.Transaction;

namespace FuelAcc.Application.UseCases.Accounting
{
    public class TransactionsService : ITransactionsService
    {
        private readonly ITransactionsRepository _repository;
        private readonly IDictionary<Guid, Rest> _cache;

        public TransactionsService(ITransactionsRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _cache = new Dictionary<Guid, Rest>();
        }

        public async Task InsertAsync(Guid documentId, DateTime date, Guid? src, Guid? dst, IEnumerable<OrderLine> lines, CancellationToken cancellationToken)
        {
            var transactions = await CreateTransactionsFromLinesAsync(documentId, date, src, dst, lines, cancellationToken);

            foreach (var transaction in transactions)
            {
                AdjustRestsOnInsert(transaction);
            }

            await _repository.InsertAsync(transactions, cancellationToken);
        }

        public async Task DeleteAsync(Guid documentId, CancellationToken cancellationToken)
        {
            var transactions = await _repository.GetAllAsync(documentId, cancellationToken);
            // adjust rests
            foreach (var transaction in transactions)
            {
                await AdjustRestsOnDeleteAsync(transaction, cancellationToken);
            }
            await _repository.DeleteAsync(transactions, cancellationToken);
        }

        public async Task UpdateAsync(Guid documentId, DateTime date, Guid? src, Guid? dst, IEnumerable<OrderLine> lines, CancellationToken cancellationToken)
        {
            var newTransactions = await CreateTransactionsFromLinesAsync(documentId, date, src, dst, lines, cancellationToken);

            var oldTransactions = await _repository.GetAllAsync(documentId, cancellationToken);

            var toDelete = new List<Transaction>();
            var toInsert = new List<Transaction>();
            var toUpdate = new List<Transaction>();

            foreach (var ot in oldTransactions)
            {
                var nt = newTransactions.FirstOrDefault(t =>
                    t.SourceId == ot.SourceId &&
                    t.DestinationId == ot.DestinationId &&
                    t.ProductId == ot.ProductId);

                if (nt != null)
                {
                    if (nt.Quantity != ot.Quantity)
                    {
                        nt.Id = ot.Id;
                        AdjustRestsOnUpdate(nt, ot.Quantity);
                        toUpdate.Add(nt);
                    }
                }
                else
                {
                    await AdjustRestsOnDeleteAsync(ot, cancellationToken);
                    toDelete.Add(ot);
                }
            }
            foreach (var nt in newTransactions)
            {
                var ot = oldTransactions.FirstOrDefault(t =>
                    t.SourceId == nt.SourceId &&
                    t.DestinationId == nt.DestinationId &&
                    t.ProductId == nt.ProductId);

                if (ot == null)
                {
                    AdjustRestsOnInsert(nt);
                    toInsert.Add(nt);
                }
            }

            await _repository.InsertAsync(toInsert, cancellationToken);
            await _repository.UpdateAsync(toUpdate, cancellationToken);
            await _repository.DeleteAsync(toDelete, cancellationToken);
        }

        private async Task AdjustRestsOnDeleteAsync(Transaction transaction, CancellationToken cancellationToken)
        {
            transaction.Source = await GetRestAsync(transaction.SourceId, cancellationToken);
            transaction.Destination = await GetRestAsync(transaction.DestinationId, cancellationToken);

            if (transaction.Source != null)
            {
                transaction.Source.Quantity += transaction.Quantity;
            }
            if (transaction.Destination != null)
            {
                if (transaction.Destination.Quantity < transaction.Quantity)
                    throw new DomainException($"Not enought quantity on destination id {transaction.DestinationId}");
                transaction.Destination.Quantity -= transaction.Quantity;
            }
        }

        private void AdjustRestsOnInsert(Transaction transaction)
        {
            if (transaction.Source != null)
            {
                if (transaction.Source.Quantity < transaction.Quantity)
                    throw new DomainException($"Not enought quantity on source id {transaction.SourceId}");
                transaction.Source.Quantity -= transaction.Quantity;
            }
            if (transaction.Destination != null)
            {
                transaction.Destination.Quantity += transaction.Quantity;
            }
        }

        private void AdjustRestsOnUpdate(Transaction transaction, decimal oldQuantity)
        {
            if (transaction.Source != null)
            {
                transaction.Source.Quantity += oldQuantity;
                if (transaction.Source.Quantity < transaction.Quantity)
                    throw new DomainException($"Not enought quantity on source id {transaction.SourceId}");
                transaction.Source.Quantity -= transaction.Quantity;
            }
            if (transaction.Destination != null)
            {
                if (transaction.Destination.Quantity < oldQuantity)
                    throw new DomainException($"Not enought quantity on destination id {transaction.DestinationId}");
                transaction.Destination.Quantity -= oldQuantity;
                transaction.Destination.Quantity += transaction.Quantity;
            }
        }

        private async Task<List<Transaction>> CreateTransactionsFromLinesAsync(Guid documentId, DateTime date, Guid? src, Guid? dst, IEnumerable<OrderLine> lines, CancellationToken cancellationToken)
        {
            if ((!src.HasValue || src.Value == Guid.Empty) && (!dst.HasValue || dst.Value == Guid.Empty))
            {
                throw new ArgumentException($"{nameof(src)} or {nameof(dst)} must be passed not null!");
            }

            if (!lines.Any())
            {
                throw new ArgumentException($"{nameof(lines)} must have at least one item!");
            }

            var transactions = new List<Transaction>();
            foreach (var line in lines)
            {
                var t = new Transaction
                {
                    Date = date,
                    OrderId = documentId,
                    Quantity = line.Quantity,
                    ProductId = line.ProductId,
                    Price = line.Price,
                };
                t.Source = await GetOrCreateRestAsync(src, line.ProductId, line.Price, cancellationToken);
                if (t.Source != null)
                {
                    t.SourceId = t.Source.Id;
                }
                t.Destination = await GetOrCreateRestAsync(dst, line.ProductId, line.Price, cancellationToken);
                if (t.Destination != null)
                {
                    t.DestinationId = t.Destination.Id;
                }

                transactions.Add(t);
            }

            return transactions;
        }

        private async Task<Rest> GetRestAsync(Guid? restId, CancellationToken cancellationToken)
        {
            if (restId.HasValue)
            {
                if (_cache.TryGetValue(restId.Value, out var cached))
                {
                    return cached;
                }
                var rest = await _repository.GetRestByIdAsync(restId.Value, cancellationToken);
                _cache[restId.Value] = rest;
                return rest;
            }
            return null;
        }

        public async Task<IEnumerable<Rest>> GetAvailableRestsAsync(Guid storageId, Guid productId, CancellationToken cancellationToken)
        {
            var rests = await _repository.GetNonEmptyRestsAsync(storageId, productId, cancellationToken);
            return rests;
        }

        private async Task<Rest?> GetOrCreateRestAsync(Guid? src, Guid productId, decimal price, CancellationToken cancellationToken)
        {
            if (src.HasValue && src.Value != Guid.Empty)
            {
                var srcRest = _cache.Values
                    .Where(e => e.StorageId == src.Value && e.ProductId == productId && e.Price == price)
                    .FirstOrDefault();

                if (srcRest != null)
                {
                    return srcRest;
                }

                srcRest = await _repository.GetRestAsync(src.Value, productId, price, cancellationToken);
                if (srcRest is null)
                {
                    srcRest = new Rest
                    {
                        StorageId = src.Value,
                        ProductId = productId,
                        Price = price,
                    };
                    await _repository.InsertRestAsync(srcRest, cancellationToken);
                }

                _cache[srcRest.Id] = srcRest;

                return srcRest;
            }
            return null;
        }
    }
}