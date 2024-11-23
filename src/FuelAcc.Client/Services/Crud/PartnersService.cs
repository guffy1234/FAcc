using FuelAcc.Application.Paging;
using FuelAcc.Client.Shared.Api;

namespace FuelAcc.Client.Services.Crud
{
    public class PartnersService : IDictionaryService<PartnerDto>
    {
        private readonly IPartnersApiClient _restClient;

        public PartnersService(IPartnersApiClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<PagedResult<PartnerDto>> GetPaged(string name, int page) =>
            (await _restClient.QueryAsync(new PartnerQueryDto { Page = page, PageSize = 5, Name = name })).ToGeneric();

        public async Task<PartnerDto> Get(Guid id) => await _restClient.ReadAsync(id);

        public async Task Delete(Guid id) => await _restClient.DeleteAsync(id);

        public async Task Add(PartnerDto dto) => await _restClient.InsertAsync(dto);

        public async Task Update(PartnerDto dto) => await _restClient.UpdateAsync(dto);
    }
}