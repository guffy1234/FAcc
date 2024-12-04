using AutoMapper;
using FuelAcc.Application.Dto.Other;
using FuelAcc.Application.Dto.Replication;
using FuelAcc.Application.Interface.Persistence;
using FuelAcc.Application.Interface.Replication;
using FuelAcc.Domain.Entities.Other;
using FuelAcc.WebApi.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FuelAcc.WebApi.Controllers.V1
{
    [ApiVersion(ApiDef.v1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsRepository _settingsRepository;
        private readonly IMapper _mapper;

        public SettingsController(ISettingsRepository settingsRepository, IMapper mapper)
        {
            _settingsRepository = settingsRepository;
            _mapper = mapper;
        }

        [HttpGet()]
        [ProducesResponseType(typeof(SettingsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetAsync(CancellationToken cancellationToken)
        {
            var settings = await _settingsRepository.GetAsync(cancellationToken);
            if (settings is null)
            {
                return NotFound();
            }
            var dto = _mapper.Map<SettingsDto>(settings);
            return Ok(dto);
        }


        [HttpPost()]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> PostAsync(SettingsDto dto, CancellationToken cancellationToken)
        {
            //todo: check the User is admin
            var settings = _mapper.Map<Settings>(dto);
            await _settingsRepository.UpsertAsync(settings, cancellationToken);
            return NoContent();
        }
    }
}