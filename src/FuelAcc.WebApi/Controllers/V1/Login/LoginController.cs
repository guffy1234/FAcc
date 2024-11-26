using FuelAcc.Application.Dto.Login;
using FuelAcc.Application.Interface.Login;
using FuelAcc.Application.UseCases.Dictionaries.Branches;
using FuelAcc.WebApi.Api;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FuelAcc.WebApi.Controllers.V1.Login
{
    [ApiVersion(ApiDef.v1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("form")]
        [ProducesResponseType(typeof(AuthResponceDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> LoginFormAsync([FromForm] AuthRequestDto dto)
        {
            var token = await _loginService.Login(dto);
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
            var token = await _loginService.Login(dto);
            if (token == null)
                return BadRequest();
            return Ok(token);
        }

        [Authorize]
        [HttpPost("logout")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> Logout()
        {
            await _loginService.Logout();
            return NoContent();
        }
    }
}