namespace FuelAcc.Application.UseCases.Documents
{
    public class OrderDto
    {
        public string Description { get; set; }
        public Guid Id { get; set; }
        public List<OrderLineDto> Lines { get; set; }
        public string Title { get; set; }
        public decimal Total { get; set; }
    }
}