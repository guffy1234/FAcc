using FuelAcc.Application.DtoCommon.Paging;

namespace FuelAcc.Application.DtoCommon.Documents
{
    public interface IDocumentQueryDto : IPagedQueryDto
    {
        DateTime? From { get; set; }
        string? Number { get; set; }
        DateTime? To { get; set; }
    }
}