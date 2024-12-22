using System.ComponentModel.DataAnnotations;

namespace FuelAcc.Application.Dto.Other
{
    public sealed class SettingsDto
    {
        [Required]
        public int Id { get; private set; }

        [Required]
        public Guid BranchId { get; set; }
    }
}