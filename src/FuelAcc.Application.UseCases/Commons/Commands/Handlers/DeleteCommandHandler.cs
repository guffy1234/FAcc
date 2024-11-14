using FuelAcc.Application.Interface;
using FuelAcc.Application.UseCases.Commons.Events;
using FuelAcc.Domain.Commons;
using FuelAcc.Domain.Entities.Other;
using MediatR;

namespace FuelAcc.Application.UseCases.Commons.Commands.Handlers
{
    public class DeleteCommandHandler<ENTITY, DTO, APOINT> : CommandHandler<DeleteCommand<DTO>>
        where DTO : class
        where ENTITY : class, IRootEntity
        where APOINT : class, IAuthorizationPoint, new()
    {
        private readonly IAuthorizationChecker _authorizationChecker;

        public DeleteCommandHandler(IMediator mediator, IAuthorizationChecker authorizationChecker) :
            base(mediator)
        {
            _authorizationChecker = authorizationChecker ?? throw new ArgumentNullException(nameof(authorizationChecker));
        }

        protected override async Task HandleAsync(DeleteCommand<DTO> command, CancellationToken cancellationToken)
        {
            var apoint = new APOINT()
            {
                Action = ApplicationAction.Insert,
            };
            _authorizationChecker.Authorize(apoint);

            var domainEvent = new DomainEvent<ENTITY>
            {
                Date = DateTime.UtcNow,
                UserId = _authorizationChecker.UserId(),
                EntityId = command.Id,
                EventAction = Domain.Enums.EventAction.Delete,
            };
            var e = new DeleteEvent<ENTITY>(domainEvent);
            await _mediator.Send(e);
        }
    }
}