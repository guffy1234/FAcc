using FuelAcc.Domain.Commons;

namespace FuelAcc.Application.UseCases.Commons.Filtering
{
    public interface IEntityFilter<ENTITY> : IEntityFilterBase where ENTITY : class, IRootEntity
    {
        IQueryable<ENTITY> BuildFilterAndSort(IQueryable<ENTITY> query);
    }
}