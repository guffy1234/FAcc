using FuelAcc.Domain.Commons;

namespace FuelAcc.Application.UseCases.Commons.Filtering
{
    public class DefaultDocumentFilter<ENTITY> : IDocumentFilter<ENTITY>
        where ENTITY : class, IDocumentEntity
    {
        public IQueryable<ENTITY> BuildFilterAndSort(IQueryable<ENTITY> query)
        {
            return query.OrderByDescending(d => d.Date);
        }
    }
}