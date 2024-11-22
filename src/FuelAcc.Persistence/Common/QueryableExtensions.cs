using FuelAcc.Domain.Commons;

namespace FuelAcc.Persistence.Common
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> TakePage<T>(this IQueryable<T> query, int pageIndex, int pageSize)
            => query.Skip(pageIndex * pageSize).Take(pageSize);

        public static IQueryable<T> OrderByName<T>(this IQueryable<T> query)
            where T : class, IDictionaryEntity
            => query.OrderBy(d => d.Name);
    }
}