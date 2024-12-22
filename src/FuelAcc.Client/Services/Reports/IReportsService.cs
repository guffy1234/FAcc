using FuelAcc.ApiClient;

namespace FuelAcc.Client.Services.Reports
{
    public interface IReportsService
    {
        Task<ICollection<ReportRestView>> GetRests(ReportRestsDto dto);

        Task<ICollection<ReportTransactionView>> GetTransactions(ReportTransactionsDto dto);
    }
}