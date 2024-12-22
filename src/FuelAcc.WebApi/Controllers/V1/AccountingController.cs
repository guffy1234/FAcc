using Asp.Versioning;
using FuelAcc.Application.Dto.Accounting;
using FuelAcc.Application.UseCases.Accounting;
using FuelAcc.WebApi.Api;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FuelAcc.WebApi.Controllers.V1
{
    [ApiVersion(ApiDef.v1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class AccountingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("availableRests")]
        [ProducesResponseType(typeof(IEnumerable<AvailableRestView>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IEnumerable<AvailableRestView>> GetTransactionsAsync(AvailableRestsDto dto, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAvailableRestsQuery(dto), cancellationToken);
            return response;
        }
    }
}