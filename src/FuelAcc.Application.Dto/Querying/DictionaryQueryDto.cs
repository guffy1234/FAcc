using FuelAcc.Application.DtoCommon.Dictionaries;

namespace FuelAcc.Application.Dto.Querying
{
    public class DictionaryQueryDto : PagedQueryDto, IDictionaryQueryDto
    {
        public string? Name { get; set; }
    }
}