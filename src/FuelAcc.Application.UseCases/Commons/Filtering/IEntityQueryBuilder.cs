using FuelAcc.Domain.Commons;

namespace FuelAcc.Application.UseCases.Commons.Filtering
{
    public interface IEntityQueryBuilder<ENTITY> : IEntityQueryBuilderBase where ENTITY : class, IRootEntity
    {
        IQueryable<ENTITY> Filter(IQueryable<ENTITY> query);

        IQueryable<ENTITY> Sort(IQueryable<ENTITY> query);
    }
}