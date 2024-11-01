namespace FuelAcc.Application.UseCases.Documents.OrdersOut
{
    public class OrderOutDto : OrderDto
    {
        public Guid PartnerId { get; set; }
        public Guid FromStorageId { get; set; }
    }
}