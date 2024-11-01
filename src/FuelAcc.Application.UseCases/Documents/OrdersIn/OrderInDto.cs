namespace FuelAcc.Application.UseCases.Documents.OrdersIn
{
    public class OrderInDto : OrderDto
    {
        public Guid PartnerId { get; set; }
        public Guid ToStorageId { get; set; }
    }
}