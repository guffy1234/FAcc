using FuelAcc.Domain.Entities.Documents;

namespace FuelAcc.Application.Interface.Accounting
{
    public interface ITransactionsService
    {
        Task InsertAsync(Guid documentId, DateTime date, Guid? src, Guid? dst, IEnumerable<OrderLine> lines, CancellationToken cancellationToken);

        Task UpdateAsync(Guid documentId, DateTime date, Guid? src, Guid? dst, IEnumerable<OrderLine> lines, CancellationToken cancellationToken);

        Task DeleteAsync(Guid documentId, CancellationToken cancellationToken);
    }
}