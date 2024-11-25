using FuelAcc.Application.DtoCommon.Paging;

namespace FuelAcc.Application.DtoCommon.Dictionaries
{
    public interface IDictionaryQueryDto : IPagedQueryDto
    {
        string? Name { get; set; }
    }
}