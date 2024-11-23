using FuelAcc.Application.Paging;

namespace FuelAcc.Client.Services.Crud
{
    public interface IDictionaryService<DTO>
    {
        Task<PagedResult<DTO>> GetPaged(string name, int page);

        Task<DTO> Get(Guid id);

        Task Delete(Guid id);

        Task Add(DTO dto);

        Task Update(DTO dto);
    }
}