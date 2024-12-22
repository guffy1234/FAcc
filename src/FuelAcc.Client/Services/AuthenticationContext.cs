using FuelAcc.Client.Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FuelAcc.Client.Services
{
    public class AuthenticationContext : IAuthenticationContext
    {
        private readonly ILocalStorageService _localStorageService;

        public bool IsAuthenticated => Principal != null;

        public ClaimsPrincipal Principal { get; private set; }
        public string Token { get; private set; }

        private string _userKey = "user";

        public bool IsInited { get; private set; }

        public AuthenticationContext(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task EnsureInited()
        {
            if (IsInited)
                return;

            var token = await _localStorageService.GetItem<string>(_userKey);
            if (!string.IsNullOrEmpty(token))
                Set(token);

            IsInited = true;
        }

        public async Task Set(string token)
        {
            Principal = ParseToken(token);
            Token = token;
            await _localStorageService.SetItem(_userKey, token);
        }

        public async Task Reset()
        {
            Principal = null;
            Token = null;
            await _localStorageService.RemoveItem(_userKey);
        }

        private static ClaimsPrincipal ParseToken(string tokenText)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(tokenText) as JwtSecurityToken;

            if (jsonToken == null)
            {
                throw new ArgumentException("Invalid JWT token");
            }

            // Create a ClaimsPrincipal from the JWT token
            var claimsIdentity = new ClaimsIdentity(jsonToken.Claims, "jwt");

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            return claimsPrincipal;
        }
    }
}