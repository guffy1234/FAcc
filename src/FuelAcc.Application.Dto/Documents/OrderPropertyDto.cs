using System.ComponentModel.DataAnnotations;

namespace FuelAcc.Application.Dto.Documents
{
    public class OrderPropertyDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Value { get; set; }
    }
}