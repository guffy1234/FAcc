using AutoMapper;
using FuelAcc.Application.Interface;
using FuelAcc.Application.UseCases.Commons.Events;
using FuelAcc.Domain.Commons;
using FuelAcc.Domain.Entities.Other;
using MediatR;

namespace FuelAcc.Application.UseCases.Commons.Commands.Handlers
{
    public partial class CreateCommandHandler<ENTITY, DTO, APOINT> : CommandHandler<CreateCommand<DTO>>
        where DTO : class
        where ENTITY : class, IRootEntity
        where APOINT : class, IAuthorizationPoint, new()
    {
        private readonly IMapper _mapper;
        private readonly IAuthorizationChecker _authorizationChecker;

        public CreateCommandHandler(IMapper mapper, IMediator mediator, IAuthorizationChecker authorizationChecker) :
            base(mediator)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _authorizationChecker = authorizationChecker ?? throw new ArgumentNullException(nameof(authorizationChecker));
        }

        protected override async Task HandleAsync(CreateCommand<DTO> command, CancellationToken cancellationToken)
        {
            var apoint = new APOINT() { 
                Action = ApplicationAction.Insert,
            };
            _authorizationChecker.Authorize(apoint);

            var entity = _mapper.Map<ENTITY>(command.Dto);
            var domainEvent = new DomainEvent<ENTITY>
            {
                Date = DateTime.Now,
                UserId = _authorizationChecker.UserId(),
                Entity = entity,
                EntityId = entity.Id,
                EventAction = Domain.Enums.EventAction.Insert
            };
            var e = new CreateEvent<ENTITY>(domainEvent);
            await _mediator.Send(e);
        }
    }
}