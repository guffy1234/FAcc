using System.ComponentModel.DataAnnotations;

namespace FuelAcc.Application.Dto.Documents
{
    public class OrderOutDto : OrderDto
    {
        [Required]
        public Guid PartnerId { get; set; }
        [Required]
        public Guid FromStorageId { get; set; }
    }
}