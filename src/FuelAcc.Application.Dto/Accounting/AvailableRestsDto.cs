using System.ComponentModel.DataAnnotations;

namespace FuelAcc.Application.Dto.Accounting
{
    public sealed class AvailableRestsDto
    {
        [Required]
        public Guid StorageId { get; set; }
        [Required]
        public Guid ProductId { get; set; }
    }

}