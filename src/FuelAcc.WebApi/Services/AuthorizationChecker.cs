using FuelAcc.Application.Interface;

namespace FuelAcc.WebApi.Services
{
    public class AuthorizationChecker : IAuthorizationChecker
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthorizationChecker(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
        }

        public Guid UserId()
        {
            var claims = _contextAccessor.HttpContext?.User.Claims;
            var claim = claims?.FirstOrDefault(c => c.Type == "UserId");
            if (claim != null && Guid.TryParse(claim.Value, out var v))
                return v;

            return Guid.Empty;
        }

        public void Authorize(IAuthorizationPoint point)
        {
            var claims = _contextAccessor.HttpContext?.User.Claims;
            if (claims != null)
            {
                var keys = new[] {
                    "admin",
                    $"A|{point.Area}|*",
                    $"A|{point.Area}|{point.Action}",
                    $"O|{point.ObjectName}|*",
                    $"O|{point.ObjectName}|{point.Action}"
                };

                foreach (var key in keys)
                {
                    if (claims.Any(c => c.Type == key && bool.TryParse(c.Value, out var v) && v))
                    {
                        return;
                    }
                }
            }
            throw new UnauthorizedAccessException();
        }
    }
}