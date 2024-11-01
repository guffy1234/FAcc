namespace FuelAcc.Application.UseCases.Documents
{
    public class OrderLineDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? Sum { get; set; }
    }
}