using FuelAcc.Application.Dto.Querying;
using FuelAcc.Domain.Commons;
using FuelAcc.Domain.Entities.Dictionaries;

namespace FuelAcc.Application.UseCases.Commons.Filtering.Handlers
{
    public class DictionaryQueryHandler<ENTITY, QUERY_DTO> : QueryBuilderHandler<QUERY_DTO>
        where ENTITY : class, IDictionaryEntity
        where QUERY_DTO : DictionaryQueryDto
    {
        public override async Task<IEntityQueryBuilderBase> Handle(QueryBuilderCommand<QUERY_DTO> request, CancellationToken cancellationToken)
        {
            var res = new DictionaryQueryBuilder<ENTITY, QUERY_DTO>(request.Dto);
            return res;
        }
    }
}