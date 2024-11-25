using FuelAcc.Application.DtoCommon.Documents;

namespace FuelAcc.Application.Dto.Querying
{
    public class DocumentQueryDto : PagedQueryDto, IDocumentQueryDto
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string? Number { get; set; }
    }
}