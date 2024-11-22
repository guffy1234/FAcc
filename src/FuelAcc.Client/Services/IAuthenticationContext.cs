namespace FuelAcc.Client.Services
{
    public interface IAuthenticationContext
    {
        string Token { get; set; }
        bool IsAuthenticated { get; }
    }
}