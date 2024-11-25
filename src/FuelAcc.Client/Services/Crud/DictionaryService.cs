using FuelAcc.Application.DtoCommon.Dictionaries;
using FuelAcc.Application.DtoCommon.Paging;
using FuelAcc.Client.Shared.Api;

namespace FuelAcc.Client.Services.Crud
{
    public class DictionaryService<DTO, PAGES_DTO, QUERY_DTO> : IDictionaryService<DTO>
        where DTO : class, IDictionaryDto
        where PAGES_DTO : class, IPagedResult<DTO>
        where QUERY_DTO : class, IDictionaryQueryDto, new()
    {
        private readonly IDtoApiClient<DTO, PAGES_DTO, QUERY_DTO> _restClient;

        public DictionaryService(IDtoApiClient<DTO, PAGES_DTO, QUERY_DTO> restClient)
        {
            _restClient = restClient;
        }

        public async Task<PagedResult<DTO>> GetPaged(string name, int page) =>
            (await _restClient.QueryAsync(new QUERY_DTO { Page = page, PageSize = 5, Name = name })).ToGeneric();

        public async Task<DTO> Get(Guid id) => await _restClient.ReadAsync(id);

        public async Task Delete(Guid id) => await _restClient.DeleteAsync(id);

        public async Task Add(DTO dto) => await _restClient.InsertAsync(dto);

        public async Task Update(DTO dto) => await _restClient.UpdateAsync(dto);
    }
}