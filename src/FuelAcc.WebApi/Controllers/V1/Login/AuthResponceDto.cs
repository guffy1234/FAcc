namespace FuelAcc.WebApi.Controllers.V1.Login
{
    public class AuthResponceDto
    {
        public string access_token { get; set; }
        public string token_type { get; set; } = "bearer";
    }
}