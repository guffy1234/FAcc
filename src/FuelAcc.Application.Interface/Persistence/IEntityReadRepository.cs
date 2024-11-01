using FuelAcc.Domain.Commons;

namespace FuelAcc.Application.Interface.Persistence
{
    public interface IEntityReadRepository<T> where T : class, IRootEntity
    {
        Task<T> GetAsync(Guid id, CancellationToken cancellationToken);

        IAsyncEnumerable<T> GetAllAsync();
    }
}