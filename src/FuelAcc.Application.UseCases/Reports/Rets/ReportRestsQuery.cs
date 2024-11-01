using FuelAcc.Application.UseCases.Commons.Queries;

namespace FuelAcc.Application.UseCases.Reports.Rets
{
    public sealed record ReportRestsQuery(ReportRestsDto dto) : Query<IAsyncEnumerable<ReportRestView>>;
}