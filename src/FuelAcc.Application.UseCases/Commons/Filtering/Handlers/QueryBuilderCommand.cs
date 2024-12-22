using FuelAcc.Application.Dto.Querying;
using MediatR;

namespace FuelAcc.Application.UseCases.Commons.Filtering.Handlers
{
    public record QueryBuilderCommand<FILTER_DTO>(FILTER_DTO Dto) : IRequest<IEntityQueryBuilderBase>
        where FILTER_DTO : PagedQueryDto;
}