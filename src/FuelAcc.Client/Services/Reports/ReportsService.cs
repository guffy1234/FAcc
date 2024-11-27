using FuelAcc.Application.DtoCommon.Dictionaries;
using FuelAcc.Application.DtoCommon.Paging;
using FuelAcc.ApiClient;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.JSInterop;

namespace FuelAcc.Client.Services.Reports
{
    public class ReportsService : IReportsService
    {
        private readonly IReportsApiClient _restClient;
        private readonly IMemoryCache _memoryCache;

        public ReportsService(IReportsApiClient restClient, IMemoryCache memoryCache)
        {
            _restClient = restClient;
            _memoryCache = memoryCache;
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