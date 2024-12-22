using FuelAcc.Domain.Entities.ReportingModels;

namespace FuelAcc.Application.Interface.Persistence
{
    public interface IReportsRepository
    {
        IAsyncEnumerable<RestReport> GetRests(bool nonEmptyOnly, IEnumerable<Guid>? storageId, IEnumerable<Guid>? productId);

        public IAsyncEnumerable<TransactionReport> GetTransactions(DateTime? from, DateTime? to,
            IEnumerable<Guid>? orderId, IEnumerable<Guid>? sourceId, IEnumerable<Guid>? destinationId, IEnumerable<Guid>? productId);
    }
}