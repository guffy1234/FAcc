using FuelAcc.Application.UseCases.Commons.Queries;
using FuelAcc.Application.UseCases.Dictionaries.Partners;
using FuelAcc.Application.UseCases.Documents.OrdersIn;
using FuelAcc.Application.UseCases.Reports.Rets;
using FuelAcc.Application.UseCases.Reports.Transactions;
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
    public class ReportsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("transactions")]
        [ProducesResponseType(typeof(IAsyncEnumerable<ReportTransactionView>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IAsyncEnumerable<ReportTransactionView>> GetTransactionsAsync(ReportTransactionsDto dto, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new ReportTransactionsQuery(dto), cancellationToken);
            return response;
        }

        [HttpPost("rests")]
        [ProducesResponseType(typeof(IAsyncEnumerable<ReportRestView>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IAsyncEnumerable<ReportRestView>> GetRestsAsync(ReportRestsDto dto, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new ReportRestsQuery(dto), cancellationToken);
            return response;
        }
    }
}