using System.ComponentModel.DataAnnotations;

namespace FuelAcc.Client.Shared
{
    public class Login
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}