using System.Security.Claims;

namespace FuelAcc.Client.Services
{
    public interface IAuthenticationContext
    {
        string Token { get; }
        ClaimsPrincipal Principal { get; }
        bool IsAuthenticated { get; }
        bool IsInited { get; }

        Task EnsureInited();

        Task Reset();

        Task Set(string token);
    }
}