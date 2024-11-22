using FuelAcc.Domain.Commons;

namespace FuelAcc.Application.UseCases.Commons.Filtering
{
    public class DefaultDictionaryFilter<ENTITY> : IDictionaryFilter<ENTITY> 
        where ENTITY : class, IDictionaryEntity 
    {
        public IQueryable<ENTITY> BuildFilterAndSort(IQueryable<ENTITY> query)
        {
            return query.OrderBy(d => d.Name);
        }
    }
}