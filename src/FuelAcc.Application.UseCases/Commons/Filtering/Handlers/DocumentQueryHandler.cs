using FuelAcc.Application.Dto.Querying;
using FuelAcc.Domain.Commons;

namespace FuelAcc.Application.UseCases.Commons.Filtering.Handlers
{
    public class DocumentQueryHandler<ENTITY, QUERY_DTO> : QueryBuilderHandler<QUERY_DTO>
        where ENTITY : class, IDocumentEntity
        where QUERY_DTO : DocumentQueryDto
    {
        public override async Task<IEntityQueryBuilderBase> Handle(QueryBuilderCommand<QUERY_DTO> request, CancellationToken cancellationToken)
        {
            var res = new DocumentQueryBuilder<ENTITY, QUERY_DTO>(request.Dto);
            return res;
        }
    }
}