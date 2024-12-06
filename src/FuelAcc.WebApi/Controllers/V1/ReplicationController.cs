using FuelAcc.Application.Dto.Querying;
using FuelAcc.Application.Dto.Replication;
using FuelAcc.Application.DtoCommon.Paging;
using FuelAcc.Application.Interface.Replication;
using FuelAcc.Application.UseCases.Commons.Queries;
using FuelAcc.WebApi.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FuelAcc.WebApi.Controllers.V1
{
    [ApiVersion(ApiDef.v1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class ReplicationController : ControllerBase
    {
        private readonly IReplicationService _replicationService;

        public ReplicationController(IReplicationService replicationService)
        {
            _replicationService = replicationService;
        }

        [HttpGet("outbound/raw/{toBranchId:guid}")]
        [ProducesResponseType(typeof(ReplictionPacketDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetOutboundRawAsync(Guid toBranchId, CancellationToken cancellationToken)
        {
            var pkt = await _replicationService.BuildOutboudPacketAsync(toBranchId, cancellationToken);
            if (pkt is null)
            {
                return NoContent();
            }
            return Ok(pkt);
        }

        [HttpGet("outbound/zip/{toBranchId:guid}")]
        [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [Produces("application/zip")]
        public async Task<IActionResult> GetOutboundZipAsync(Guid toBranchId, CancellationToken cancellationToken)
        {
            var zip = await _replicationService.BuildOutboudZipAsync(toBranchId, cancellationToken);
            if (zip is null)
            {
                return NoContent();
            }
            return File(zip.Value.Data, "application/zip", zip.Value.FileName);
        }

        [HttpPost("inbound/raw")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> PostInboundRawAsync(ReplictionPacketDto pkt, CancellationToken cancellationToken)
        {
            await _replicationService.ApplyInboundPacketAsync(pkt, cancellationToken);
            return NoContent();
        }

        [HttpPost("inbound/zip")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> PostInboundZipAsync([FromForm] FileUploadModel model, CancellationToken cancellationToken)
        {
            if (model?.File == null || model.File.Length <= 0)
            {
                return BadRequest("No file was uploaded.");
            }

            using (var mstm = new MemoryStream())
            {
                await model.File.CopyToAsync(mstm);

                var data = mstm.ToArray();

                await _replicationService.ApplyInboundZipAsync(data, cancellationToken);
            }
            return NoContent();
        }

        [HttpPost("query")]
        [ProducesResponseType(typeof(PagedResult<ReplictionPacketViewDto>), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [Authorize]
        public async Task<PagedResult<ReplictionPacketViewDto>> InternalGetPagedAsync([FromBody] ReplicationQueryDto dto, CancellationToken cancellationToken)
        {
            var response = await _replicationService.GetPagedHistoryAsync(dto, cancellationToken);
            return response;
        }
    }
}