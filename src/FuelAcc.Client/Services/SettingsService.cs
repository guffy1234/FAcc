using FuelAcc.ApiClient;
using Microsoft.Extensions.Caching.Memory;

namespace FuelAcc.Client.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly ISettingsApiClient _restClient;
        private readonly IMemoryCache _memoryCache;

        public SettingsService(ISettingsApiClient restClient, IMemoryCache memoryCache)
        {
            _restClient = restClient;
            _memoryCache = memoryCache;
        }

        public async Task<Guid> GetCurrentBranchId()
        {
            var key = "DbSettings";

            var state = await _memoryCache.GetOrCreateAsync<SettingsDto>(key, async entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

                var dtos = await _restClient.SettingsGetAsync();
                return dtos;
            });
            return state.BranchId;
        }
    }
}