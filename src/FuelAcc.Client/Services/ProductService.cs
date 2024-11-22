using FuelAcc.Application.Paging;
using FuelAcc.Client.Shared.Api;

namespace FuelAcc.Client.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductsApiClient _restClient;

        public ProductService(IProductsApiClient restClient)
        {
            this._restClient = restClient;
        }

        public async Task<PagedResult<ProductDto>> GetPaged(string name, int page)
        {
            var res = await _restClient.Paged6Async(page, null);

            return new PagedResult<ProductDto>()
            {
                CurrentPage = res.CurrentPage,
                PageSize = res.PageSize,
                PageCount = res.PageCount,
                RowCount = res.RowCount,
                Results = res.Results.ToList()
            };
        }

        public async Task<ProductDto> Get(Guid id)
        {
            return await _restClient.ProductsGETAsync(id);
        }

        public async Task Delete(Guid id)
        {
            await _restClient.ProductsDELETEAsync(id);
        }

        public async Task Add(ProductDto dto)
        {
            await _restClient.ProductsPOSTAsync(dto);
        }

        public async Task Update(ProductDto dto)
        {
            await _restClient.ProductsPUTAsync(dto);
        }
    }
}