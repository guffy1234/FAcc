using FuelAcc.Domain.Enums;

namespace FuelAcc.Application.UseCases.Documents.OrdersMove
{
    public class OrderMoveDto : OrderDto
    {
        public Guid? FromStorageId { get; set; }
        public Guid? ToStorageId { get; set; }
        public OrderMoveType MoveType { get; set; }
    }
}