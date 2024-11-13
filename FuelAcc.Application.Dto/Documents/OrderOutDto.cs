namespace FuelAcc.Application.Dto.Documents
{
    public class OrderOutDto : OrderDto
    {
        public Guid PartnerId { get; set; }
        public Guid FromStorageId { get; set; }
    }
}