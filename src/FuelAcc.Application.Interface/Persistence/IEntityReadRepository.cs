using FuelAcc.Domain.Commons;

namespace FuelAcc.Application.Interface.Persistence
{
    public interface IEntityReadRepository<T> where T : class, IRootEntity
    {
        Task<T> GetAsync(Guid id, CancellationToken cancellationToken);

        IAsyncEnumerable<T> GetAllAsync(bool asNoTracked = true);

        Task<(int Total, IList<T> Items)> GetExtendedAsync(Func<IQueryable<T>, IQueryable<T>> filter, int page, int pageSize, bool asNoTracked, CancellationToken cancellationToken);
    }
}