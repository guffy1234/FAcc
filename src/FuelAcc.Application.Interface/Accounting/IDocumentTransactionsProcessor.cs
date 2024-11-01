using FuelAcc.Domain.Commons;

namespace FuelAcc.Application.Interface.Accounting
{
    public interface IDocumentTransactionsProcessor<T> where T : class, IDocumentEntity
    {
        Task InsertAsync(T entity, CancellationToken cancellationToken);

        Task UpdateAsync(T entity, CancellationToken cancellationToken);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}