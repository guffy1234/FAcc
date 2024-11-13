namespace FuelAcc.Application.Dto.Documents
{
    public class OrderInDto : OrderDto
    {
        public Guid PartnerId { get; set; }
        public Guid ToStorageId { get; set; }
    }
}