namespace FuelAcc.Client.Services
{
    public class AuthenticationContext : IAuthenticationContext
    {
        public string Token { get; set; }

        public bool IsAuthenticated => Token != null;

        public AuthenticationContext()
        {
        }
    }
}