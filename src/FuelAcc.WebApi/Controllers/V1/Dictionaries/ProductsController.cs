using FuelAcc.Application.Dto;
using FuelAcc.Application.Dto.Dictionaries;
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
    public class ProductsController : EntityControllerBase<ProductDto>
    {
        public ProductsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(IAsyncEnumerable<ProductDto>), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IAsyncEnumerable<ProductDto>> GetAllAsync(CancellationToken cancellationToken) =>
            await InternalGetAllAsync(cancellationToken);

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResult<ProductDto>), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [Authorize]
        public async Task<PagedResult<ProductDto>> GetPagedAsync([FromQuery] int? page, int? pageSize, CancellationToken cancellationToken) =>
            await InternalGetPagedAsync(page, pageSize, cancellationToken);

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetAsync(Guid id, CancellationToken cancellationToken) =>
            await InternalGetAsync(id, cancellationToken);

        [HttpPost]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<ActionResult> InsertAsync([FromBody] ProductDto dto, CancellationToken cancellationToken) =>
            await InternalInsertAsync(dto, cancellationToken);

        [HttpPut]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<ActionResult> UpdateAsync([FromBody] ProductDto dto, CancellationToken cancellationToken) =>
            await InternalUpdateAsync(dto, cancellationToken);

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<ActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken) =>
            await InternalDeleteAsync(id, cancellationToken);
    }
}