namespace FuelAcc.Application.Dto.Querying
{
    public class DocumentQueryDto : PagedQueryDto
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string? Number { get; set; }
    }
}