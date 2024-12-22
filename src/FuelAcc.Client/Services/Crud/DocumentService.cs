using FuelAcc.ApiClient;
using FuelAcc.Application.DtoCommon.Documents;
using FuelAcc.Application.DtoCommon.Paging;

namespace FuelAcc.Client.Services.Crud
{
    public class DocumentService<DTO, PAGES_DTO, QUERY_DTO> : IDocumentService<DTO>
        where DTO : class, IDocumentDto
        where PAGES_DTO : class, IPagedResult<DTO>
        where QUERY_DTO : class, IDocumentQueryDto, new()
    {
        private readonly IDtoApiClient<DTO, PAGES_DTO, QUERY_DTO> _restClient;

        public DocumentService(IDtoApiClient<DTO, PAGES_DTO, QUERY_DTO> restClient)
        {
            _restClient = restClient;
        }

        public async Task<PagedResult<DTO>> GetPaged(int page) =>
            (await _restClient.QueryAsync(new QUERY_DTO { Page = page, PageSize = 5 })).ToGeneric();

        public async Task<DTO> Get(Guid id) => await _restClient.ReadAsync(id);

        public async Task Delete(Guid id) => await _restClient.DeleteAsync(id);

        public async Task Add(DTO dto) => await _restClient.InsertAsync(dto);

        public async Task Update(DTO dto) => await _restClient.UpdateAsync(dto);
    }
}