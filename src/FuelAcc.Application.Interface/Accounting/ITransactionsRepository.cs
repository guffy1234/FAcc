using FuelAcc.Domain.Entities.Registry;

namespace FuelAcc.Application.Interface.Accounting
{
    public interface ITransactionsRepository
    {
        Task<Rest> GetRestByIdAsync(Guid id, CancellationToken cancellationToken);

        Task InsertRestAsync(Rest rest, CancellationToken cancellationToken);

        Task<IEnumerable<Transaction>> GetAllAsync(Guid documentId, CancellationToken cancellationToken);

        Task InsertAsync(IEnumerable<Transaction> transactions, CancellationToken cancellationToken);

        Task DeleteAsync(IEnumerable<Transaction> transactions, CancellationToken cancellationToken);

        Task UpdateAsync(IEnumerable<Transaction> transactions, CancellationToken cancellationToken);
        Task<Rest> GetRestAsync(Guid storageId, Guid productId, decimal price, CancellationToken cancellationToken);
        Task<IEnumerable<Rest>> GetNonEmptyRestsAsync(Guid storageId, Guid productId, CancellationToken cancellationToken);
    }
}