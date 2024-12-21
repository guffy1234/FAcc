using FuelAcc.Domain.Entities.Documents;
using FuelAcc.Domain.Entities.Registry;

namespace FuelAcc.Application.Interface.Accounting
{
    public interface ITransactionsService
    {
        Task InsertAsync(Guid documentId, DateTime date, Guid? src, Guid? dst, IEnumerable<OrderLine> lines, CancellationToken cancellationToken);

        Task UpdateAsync(Guid documentId, DateTime date, Guid? src, Guid? dst, IEnumerable<OrderLine> lines, CancellationToken cancellationToken);

        Task DeleteAsync(Guid documentId, CancellationToken cancellationToken);
        Task<IEnumerable<Rest>> GetAvailableRestsAsync(Guid storageId, Guid productId, CancellationToken cancellationToken);
    }
}