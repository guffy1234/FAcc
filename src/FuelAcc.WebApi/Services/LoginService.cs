using FuelAcc.Application.Dto.Login;
using FuelAcc.Application.Interface.Login;
using FuelAcc.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FuelAcc.WebApi.Services
{
    public class LoginService : ILoginService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task Logout()
        {
            //await _signInManager.SignOutAsync();
        }

        public async Task<AuthResponceDto?> Login(AuthRequestDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.username);
            if (user != null && await _userManager.CheckPasswordAsync(user, dto.password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim("UserId", user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var userClaims = await _userManager.GetClaimsAsync(user);
                if (userClaims != null)
                    authClaims.AddRange(userClaims);

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));

                    var role = await _roleManager.FindByNameAsync(userRole);
                    if (role != null)
                    {
                        var roleClaims = await _roleManager.GetClaimsAsync(role);
                        if (roleClaims != null)
                            authClaims.AddRange(roleClaims);
                    }
                }

                var prepared = authClaims.Distinct().ToList();

                var token = GetToken(prepared);

                var tokenText = new JwtSecurityTokenHandler().WriteToken(token);

                //await _signInManager.SignInAsync(user, true);

                return new AuthResponceDto
                {
                    access_token = tokenText
                };
            }
            return null;
        }

        private JwtSecurityToken GetToken(IEnumerable<Claim> authClaims)
        {
            var authSigningKey = AuthOptions.GetSymmetricSecurityKey();

            var token = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                expires: DateTime.Now.AddMinutes(AuthOptions.LIFETIME), //need UTC??
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}