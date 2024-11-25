using FuelAcc.Application.DtoCommon.Paging;

namespace FuelAcc.Client.Services.Crud
{
    public interface IDocumentService<DTO>
    {
        Task<PagedResult<DTO>> GetPaged(int page);

        Task<DTO> Get(Guid id);

        Task Delete(Guid id);

        Task Add(DTO dto);

        Task Update(DTO dto);
    }
}