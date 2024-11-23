using FuelAcc.Application.Dto.Querying;
using MediatR;

namespace FuelAcc.Application.UseCases.Commons.Filtering.Handlers
{

    public abstract class QueryBuilderHandler<QUERY_DTO> : IRequestHandler<QueryBuilderCommand<QUERY_DTO>, IEntityQueryBuilderBase>
        where QUERY_DTO : PagedQueryDto
    {
        protected QueryBuilderHandler()
        {
        }

        public abstract Task<IEntityQueryBuilderBase> Handle(QueryBuilderCommand<QUERY_DTO> request, CancellationToken cancellationToken);
    }
}