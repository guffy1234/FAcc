using FuelAcc.Application.Dto.Querying;
using FuelAcc.Domain.Commons;

namespace FuelAcc.Application.UseCases.Commons.Filtering
{
    public class DocumentQueryBuilder<ENTITY, QUERY_DTO> : IDocumentQueryBuilder<ENTITY>
       where ENTITY : class, IDocumentEntity
       where QUERY_DTO : DocumentQueryDto
    {
        public DocumentQueryBuilder(QUERY_DTO dto)
        {
            Dto = dto;
        }

        public QUERY_DTO Dto { get; }

        public int? Page => Dto.Page;

        public int? PageSize => Dto.PageSize;

        public IQueryable<ENTITY> Sort(IQueryable<ENTITY> query)
        {
            return query.OrderBy(d => d.Date);
        }

        IQueryable<ENTITY> IEntityQueryBuilder<ENTITY>.Filter(IQueryable<ENTITY> query)
        {
            if (Dto.From.HasValue)
            {
                query = query.Where(d => d.Date >= Dto.From);
            }
            if (Dto.To.HasValue)
            {
                query = query.Where(d => d.Date <= Dto.To);
            }
            return query;
        }
    }
}