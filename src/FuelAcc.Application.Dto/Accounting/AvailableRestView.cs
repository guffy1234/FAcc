using System.ComponentModel.DataAnnotations;

namespace FuelAcc.Application.Dto.Accounting
{
    public sealed class AvailableRestView
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid StorageId { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public decimal Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}