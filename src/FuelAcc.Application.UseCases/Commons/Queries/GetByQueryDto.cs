using FuelAcc.Application.Dto;
using FuelAcc.Application.Dto.Querying;
using FuelAcc.Application.Paging;
using FuelAcc.Application.UseCases.Commons.Filtering;

namespace FuelAcc.Application.UseCases.Commons.Queries
{
    public sealed record GetByQueryDto<DTO, QUERY_DTO>(QUERY_DTO dto) : Query<PagedResult<DTO>>
        where QUERY_DTO: PagedQueryDto ;
}