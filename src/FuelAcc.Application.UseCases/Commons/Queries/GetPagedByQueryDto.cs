using FuelAcc.Application.Dto.Querying;
using FuelAcc.Application.DtoCommon.Paging;

namespace FuelAcc.Application.UseCases.Commons.Queries
{
    public sealed record GetPagedByQueryDto<DTO, QUERY_DTO>(QUERY_DTO dto) : Query<PagedResult<DTO>>
        where QUERY_DTO : PagedQueryDto;
}