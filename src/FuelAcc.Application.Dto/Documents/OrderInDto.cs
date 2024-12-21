using System.ComponentModel.DataAnnotations;

namespace FuelAcc.Application.Dto.Documents
{
    public class OrderInDto : OrderDto
    {
        [Required]
        public Guid PartnerId { get; set; }
        [Required]
        public Guid ToStorageId { get; set; }
    }
}