using FuelAcc.WebApi.Api;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FuelAcc.WebApi.Controllers.V1
{
    [ApiVersion(ApiDef.v1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ReplicationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReplicationController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}