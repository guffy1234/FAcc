using System.ComponentModel.DataAnnotations;

namespace FuelAcc.Application.Dto.Login
{
    public class AuthRequestDto
    {
        public string? grant_type { get; set; }
        public string? scope { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
        public string? client_id { get; set; }
        public string? client_secret { get; set; }
    }
}