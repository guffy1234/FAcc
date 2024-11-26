using FuelAcc.Application.DtoCommon.Paging;

namespace FuelAcc.Client.Services.Crud
{
    public interface IDictionaryService<DTO>
    {
        Task<PagedResult<DTO>> GetPaged(string name, int page);

        Task<DTO> Get(Guid id);

        Task Delete(Guid id);

        Task Add(DTO dto);

        Task Update(DTO dto);
        Task<string> LookupName(Guid id);
        void ResetLookup();
        Task<IReadOnlyCollection<KeyValuePair<Guid, string>>> LookupItems();
    }
}