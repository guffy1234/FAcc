﻿using FuelAcc.Application.UseCases.Commons.Commands;
using FuelAcc.Application.UseCases.Commons.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FuelAcc.WebApi.Controllers
{
    [Authorize]
    public class EntityControllerBase<DTO> : ControllerBase
    {
        protected readonly IMediator _mediator;

        protected EntityControllerBase(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected async Task<IAsyncEnumerable<DTO>> InternalGetAllAsync(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAllQuery<DTO>(), cancellationToken);
            return response;
        }

        protected async Task<IActionResult> InternalGetAsync(Guid id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetByIdQuery<DTO>(id), cancellationToken);
            return Ok(response);
        }

        protected async Task<ActionResult> InternalInsertAsync([FromBody] DTO dto, CancellationToken cancellationToken)
        {
            await _mediator.Send(new CreateCommand<DTO>(dto), cancellationToken);
            return NoContent();
        }

        protected async Task<ActionResult> InternalUpdateAsync([FromBody] DTO dto, CancellationToken cancellationToken)
        {
            await _mediator.Send(new UpdateCommand<DTO>(dto), cancellationToken);
            return NoContent();
        }

        protected async Task<ActionResult> InternalDeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteCommand<DTO>(id), cancellationToken);
            return NoContent();
        }
    }
}