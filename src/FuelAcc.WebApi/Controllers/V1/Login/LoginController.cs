using FuelAcc.Application.UseCases.Dictionaries.Branches;
using FuelAcc.Domain.Identity;
using FuelAcc.WebApi.Api;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FuelAcc.WebApi.Controllers.V1.Login
{
    [ApiVersion(ApiDef.v1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public LoginController(IMediator mediator, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _mediator = mediator;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost("form")]
        [ProducesResponseType(typeof(AuthResponceDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> LoginFormAsync([FromForm] AuthRequestDto dto)
        {
            var token = await DoLogin(dto);
            if (token == null)
                return BadRequest();
            return Ok(token);
        }

        [HttpPost("json")]
        [ProducesResponseType(typeof(AuthResponceDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> LoginJsonAsync(AuthRequestDto dto)
        {
            var token = await DoLogin(dto);
            if (token == null)
                return BadRequest();
            return Ok(token);
        }

        private async Task<AuthResponceDto?> DoLogin(AuthRequestDto dto)
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