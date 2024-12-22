using System.ComponentModel.DataAnnotations;

namespace FuelAcc.Application.Dto.Login
{
    public class AuthResponceDto
    {
        [Required]
        public string access_token { get; set; }

        [Required]
        public string token_type { get; set; } = "bearer";
    }
}