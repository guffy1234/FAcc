using FuelAcc.Domain.Commons;

namespace FuelAcc.Application.Interface.Persistence
{
    public interface IEntityWriteRepository<T> where T : class, IRootEntity
    {
        Task InsertAsync(T entity, CancellationToken cancellationToken);

        Task UpdateAsync(T entity, CancellationToken cancellationToken);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}