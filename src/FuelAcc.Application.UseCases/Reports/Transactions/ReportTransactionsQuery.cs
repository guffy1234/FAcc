using FuelAcc.Application.UseCases.Commons.Queries;

namespace FuelAcc.Application.UseCases.Reports.Transactions
{
    public sealed record ReportTransactionsQuery(ReportTransactionsDto dto) : Query<IAsyncEnumerable<ReportTransactionView>>;
}