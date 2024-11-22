using FuelAcc.Application.Dto;
using FuelAcc.Application.Dto.Dictionaries;
using FuelAcc.WebApi.Api;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FuelAcc.WebApi.Controllers.V1.Dictionaries
{
    /// <summary>
    /// REST API for manage Branches distionary
    /// </summary>
    [ApiVersion(ApiDef.v1)]
    [Route("api/v{version:apiVersion}/dictionaries/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class BranchesController : EntityControllerBase<BranchDto>
    {
        public BranchesController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Get all branches
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("all")]
        [ProducesResponseType(typeof(IAsyncEnumerable<BranchDto>), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [Authorize]
        public async Task<IAsyncEnumerable<BranchDto>> GetAllAsync(CancellationToken cancellationToken) =>
            await InternalGetAllAsync(cancellationToken);

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResult<BranchDto>), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [Authorize]
        public async Task<PagedResult<BranchDto>> GetPagedAsync([FromQuery] int? page, int? pageSize, CancellationToken cancellationToken) =>
           await InternalGetPagedAsync(page, pageSize, cancellationToken);

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(BranchDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetAsync(Guid id, CancellationToken cancellationToken) =>
            await InternalGetAsync(id, cancellationToken);

        [HttpPost]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<ActionResult> InsertAsync([FromBody] BranchDto dto, CancellationToken cancellationToken) =>
            await InternalInsertAsync(dto, cancellationToken);

        [HttpPut]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<ActionResult> UpdateAsync([FromBody] BranchDto dto, CancellationToken cancellationToken) =>
            await InternalUpdateAsync(dto, cancellationToken);

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<ActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken) =>
            await InternalDeleteAsync(id, cancellationToken);
    }
}