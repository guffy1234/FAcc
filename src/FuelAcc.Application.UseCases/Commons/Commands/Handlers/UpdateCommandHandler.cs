using AutoMapper;
using FuelAcc.Application.Interface;
using FuelAcc.Application.UseCases.Commons.Events;
using FuelAcc.Domain.Commons;
using FuelAcc.Domain.Entities.Other;
using MediatR;

namespace FuelAcc.Application.UseCases.Commons.Commands.Handlers
{
    public class UpdateCommandHandler<ENTITY, DTO, APOINT> : CommandHandler<UpdateCommand<DTO>>
        where DTO : class
        where ENTITY : class, IRootEntity
        where APOINT : class, IAuthorizationPoint, new()
    {
        private readonly IMapper _mapper;
        private readonly IAuthorizationChecker _authorizationChecker;

        public UpdateCommandHandler(IMapper mapper, IMediator mediator, IAuthorizationChecker authorizationChecker) :
            base(mediator)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _authorizationChecker = authorizationChecker ?? throw new ArgumentNullException(nameof(authorizationChecker));
        }

        protected override async Task HandleAsync(UpdateCommand<DTO> command, CancellationToken cancellationToken)
        {
            var apoint = new APOINT()
            {
                Action = ApplicationAction.Delete,
            };
            _authorizationChecker.Authorize(apoint);

            var entity = _mapper.Map<ENTITY>(command.Dto);
            var domainEvent = new DomainEvent<ENTITY>
            {
                Date = DateTime.UtcNow,
                UserId = _authorizationChecker.UserId(),
                Entity = entity,
                EntityId = entity.Id,
                EventAction = Domain.Enums.EventAction.Update,
            };
            var e = new UpdateEvent<ENTITY>(domainEvent);
            await _mediator.Send(e);
        }
    }
}