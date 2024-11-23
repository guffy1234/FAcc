﻿using FuelAcc.Application.Dto.Dictionaries;
using FuelAcc.Application.Dto;
using FuelAcc.Application.Dto.Documents;
using FuelAcc.WebApi.Api;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FuelAcc.Application.Paging;
using FuelAcc.Application.Dto.Querying;

namespace FuelAcc.WebApi.Controllers.V1.Orders
{
    [ApiVersion(ApiDef.v1)]
    [Route("api/v{version:apiVersion}/orders/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class OrdersInController : EntityControllerBase<OrderInDto, OrderInQueryDto>
    {
        public OrdersInController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(IAsyncEnumerable<OrderInDto>), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IAsyncEnumerable<OrderInDto>> GetAllAsync(CancellationToken cancellationToken) =>
            await InternalGetAllAsync(cancellationToken);

        [HttpPost("query")]
        [ProducesResponseType(typeof(PagedResult<OrderInDto>), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [Authorize]
        public async Task<PagedResult<OrderInDto>> GetPagedAsync([FromBody] OrderInQueryDto dto, CancellationToken cancellationToken) =>
            await InternalGetPagedAsync(dto, cancellationToken);

        [HttpGet("read/{id:guid}")]
        [ProducesResponseType(typeof(OrderInDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetAsync(Guid id, CancellationToken cancellationToken) =>
            await InternalGetAsync(id, cancellationToken);

        [HttpPost("insert")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<ActionResult> InsertAsync([FromBody] OrderInDto dto, CancellationToken cancellationToken) =>
            await InternalInsertAsync(dto, cancellationToken);

        [HttpPut("update")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<ActionResult> UpdateAsync([FromBody] OrderInDto dto, CancellationToken cancellationToken) =>
            await InternalUpdateAsync(dto, cancellationToken);

        [HttpDelete("delete/{id:guid}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<ActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken) =>
            await InternalDeleteAsync(id, cancellationToken);
    }
}