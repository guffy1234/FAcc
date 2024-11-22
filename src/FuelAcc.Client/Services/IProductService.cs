using FuelAcc.Application.Paging;
using FuelAcc.Client.Shared.Api;

namespace FuelAcc.Client.Services
{
    public interface IProductService
    {
        Task<PagedResult<ProductDto>> GetPaged(string name, int page);
        Task<ProductDto> Get(Guid id);

        Task Delete(Guid id);

        Task Add(ProductDto dto);

        Task Update(ProductDto dto);
    }
}