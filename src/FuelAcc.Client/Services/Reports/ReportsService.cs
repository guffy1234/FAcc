using FuelAcc.ApiClient;

namespace FuelAcc.Client.Services.Reports
{
    public class ReportsService : IReportsService
    {
        private readonly IReportsApiClient _restClient;

        public ReportsService(IReportsApiClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<ICollection<ReportTransactionView>> GetTransactions(ReportTransactionsDto dto)
        {
            var result = await _restClient.TransactionsAsync(dto);
            return result;
        }

        public async Task<ICollection<ReportRestView>> GetRests(ReportRestsDto dto)
        {
            var result = await _restClient.RestsAsync(dto);
            return result;
        }
    }
}