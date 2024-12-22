using FuelAcc.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FuelAcc.Application.Dto.Documents
{
    public class OrderMoveDto : OrderDto
    {
        public Guid? FromStorageId { get; set; }
        public Guid? ToStorageId { get; set; }

        [Required]
        public OrderMoveType MoveType { get; set; }
    }
}