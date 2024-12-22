using FuelAcc.ApiClient;
using FuelAcc.Application.DtoCommon.Dictionaries;
using FuelAcc.Application.DtoCommon.Paging;
using Microsoft.Extensions.Caching.Memory;

namespace FuelAcc.Client.Services.Crud
{
    public class DictionaryService<DTO, PAGES_DTO, QUERY_DTO> : IDictionaryService<DTO>
        where DTO : class, IDictionaryDto
        where PAGES_DTO : class, IPagedResult<DTO>
        where QUERY_DTO : class, IDictionaryQueryDto, new()
    {
        private readonly IDtoApiClient<DTO, PAGES_DTO, QUERY_DTO> _restClient;
        private readonly IMemoryCache _memoryCache;

        public DictionaryService(IDtoApiClient<DTO, PAGES_DTO, QUERY_DTO> restClient, IMemoryCache memoryCache)
        {
            _restClient = restClient;
            _memoryCache = memoryCache;
        }

        public async Task<PagedResult<DTO>> GetPaged(string name, int page) =>
            (await _restClient.QueryAsync(new QUERY_DTO { Page = page, PageSize = 5, Name = name })).ToGeneric();

        public async Task<DTO> Get(Guid id) => await _restClient.ReadAsync(id);

        public async Task Delete(Guid id)
        {
            await _restClient.DeleteAsync(id);
            ResetLookup();
        }

        public async Task Add(DTO dto)
        {
            await _restClient.InsertAsync(dto);
            ResetLookup();
        }

        public async Task Update(DTO dto)
        {
            await _restClient.UpdateAsync(dto);
            ResetLookup();
        }

        public async Task<string> LookupName(Guid id)
        {
            var state = await GetLookupState();

            if (state.Lookup.TryGetValue(id, out var name))
                return name;

            return string.Empty;
        }

        public async Task<IReadOnlyCollection<KeyValuePair<Guid, string>>> LookupItems()
        {
            var state = await GetLookupState();

            return state.Sorted.AsReadOnly();
        }

        private class CacheState
        {
            public Dictionary<Guid, string> Lookup { get; set; }
            public List<KeyValuePair<Guid, string>> Sorted { get; set; }
        }

        private async Task<CacheState> GetLookupState()
        {
            var key = BuildCacheKey();

            var state = await _memoryCache.GetOrCreateAsync<CacheState>(key, async entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

                var dtos = await _restClient.AllAsync();
                var result = new CacheState();
                result.Lookup = dtos.Select(d => new KeyValuePair<Guid, string>(d.Id, d.Name)).ToDictionary();
                result.Sorted = result.Lookup.OrderBy(d => d.Value).ToList();
                return result;
            });
            return state;
        }

        public void ResetLookup()
        {
            var key = BuildCacheKey();

            _memoryCache.Remove(key);
        }

        private static string BuildCacheKey()
        {
            return $"Dictionary.Lookup.{typeof(DTO).Name}";
        }
    }
}