using System.ComponentModel.DataAnnotations;

namespace FuelAcc.Application.Dto.Documents
{
    public class OrderLineDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public decimal PlannedQuantity { get; set; }
        [Required]
        public decimal Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public decimal Sum { get; set; }
    }
}