using FuelAcc.Application.Paging;
using FuelAcc.Client.Shared.Api;

namespace FuelAcc.Client.Services.Crud
{
    public class ProductService : IDictionaryService<ProductDto>
    {
        private readonly IProductsApiClient _restClient;

        public ProductService(IProductsApiClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<PagedResult<ProductDto>> GetPaged(string name, int page) => 
            (await _restClient.QueryAsync(new ProductQueryDto { Page = page, PageSize = 5, Name = name })).ToGeneric();

        public async Task<ProductDto> Get(Guid id) => await _restClient.ReadAsync(id);

        public async Task Delete(Guid id) => await _restClient.DeleteAsync(id);

        public async Task Add(ProductDto dto) => await _restClient.InsertAsync(dto);

        public async Task Update(ProductDto dto) => await _restClient.UpdateAsync(dto);
    }
}