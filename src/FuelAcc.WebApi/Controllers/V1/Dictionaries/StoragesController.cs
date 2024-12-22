using Asp.Versioning;
using FuelAcc.Application.Dto.Dictionaries;
using FuelAcc.Application.Dto.Querying;
using FuelAcc.Application.DtoCommon.Paging;
using FuelAcc.WebApi.Api;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FuelAcc.WebApi.Controllers.V1.Dictionaries
{
    [ApiVersion(ApiDef.v1)]
    [Route("api/v{version:apiVersion}/dictionaries/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class StoragesController : EntityControllerBase<StorageDto, StorageQueryDto>
    {
        public StoragesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(IAsyncEnumerable<StorageDto>), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IAsyncEnumerable<StorageDto>> GetAllAsync(CancellationToken cancellationToken) =>
            await InternalGetAllAsync(cancellationToken);

        [HttpPost("query")]
        [ProducesResponseType(typeof(PagedResult<StorageDto>), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [Authorize]
        public async Task<PagedResult<StorageDto>> GetPagedAsync([FromBody] StorageQueryDto dto, CancellationToken cancellationToken) =>
            await InternalGetPagedAsync(dto, cancellationToken);

        [HttpGet("read/{id:guid}")]
        [ProducesResponseType(typeof(StorageDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetAsync(Guid id, CancellationToken cancellationToken) =>
            await InternalGetAsync(id, cancellationToken);

        [HttpPost("insert")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<ActionResult> InsertAsync([FromBody] StorageDto dto, CancellationToken cancellationToken) =>
            await InternalInsertAsync(dto, cancellationToken);

        [HttpPut("update")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<ActionResult> UpdateAsync([FromBody] StorageDto dto, CancellationToken cancellationToken) =>
            await InternalUpdateAsync(dto, cancellationToken);

        [HttpDelete("delete/{id:guid}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<ActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken) =>
            await InternalDeleteAsync(id, cancellationToken);
    }
}