using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace FuelAcc.Client.Services
{
    public class IdentityAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IAuthorizeApi _authorizeApi;
        private readonly IAuthenticationContext _context;

        public IdentityAuthenticationStateProvider(IAuthorizeApi authorizeApi, IAuthenticationContext context)
        {
            _authorizeApi = authorizeApi;
            _context = context;
        }

        public async Task Login(LoginParameters loginParameters)
        {
            var token = await _authorizeApi.Login(loginParameters);
            await _context.Set(token);

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task Logout()
        {
            await _authorizeApi.Logout();
            await _context.Reset();

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            await _context.EnsureInited();

            var principal = _context.Principal ?? new ClaimsPrincipal(new ClaimsIdentity());

            return new AuthenticationState(principal);
        }
    }
}