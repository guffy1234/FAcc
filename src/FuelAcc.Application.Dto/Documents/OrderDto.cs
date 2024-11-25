using FuelAcc.Application.DtoCommon.Documents;

namespace FuelAcc.Application.Dto.Documents
{
    public class OrderDto : IDocumentDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Number { get; set; }
        public List<OrderLineDto> Lines { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public decimal Total { get; set; }
    }
}