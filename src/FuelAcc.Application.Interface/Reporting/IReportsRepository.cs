

using FuelAcc.Domain.Entities.Registry;

namespace FuelAcc.Application.Interface.Persistence
{
    public interface IReportsRepository
    {
        IAsyncEnumerable<Rest> GetRests(bool nonEmptyOnly, IEnumerable<Guid>? storageId, IEnumerable<Guid>? productId);
        public IAsyncEnumerable<Transaction> GetTransactions(DateTime? from, DateTime? to,
            IEnumerable<Guid>? orderId, IEnumerable<Guid>? sourceId, IEnumerable<Guid>? destinationId, IEnumerable<Guid>? productId);
    }
}