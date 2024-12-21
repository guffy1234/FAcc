using FuelAcc.Application.Dto.Accounting;
using FuelAcc.Application.UseCases.Commons.Queries;

namespace FuelAcc.Application.UseCases.Accounting
{
    public record GetAvailableRestsQuery(AvailableRestsDto Dto) : Query<IEnumerable<AvailableRestView>>;
}