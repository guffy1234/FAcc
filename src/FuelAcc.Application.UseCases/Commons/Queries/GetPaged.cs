using FuelAcc.Application.Dto;
using FuelAcc.Application.UseCases.Commons.Filtering;

namespace FuelAcc.Application.UseCases.Commons.Queries
{
    public sealed record GetPaged<DTO>(int Page, int PageSize, IEntityFilterBase Filter = null) : Query<PagedResult<DTO>>;
}