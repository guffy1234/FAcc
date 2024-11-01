using MediatR;

namespace FuelAcc.Application.UseCases.Commons.Commands.Handlers
{
    public abstract class CommandHandler<TCommand> : CommandHandler, IRequestHandler<TCommand, Unit> where TCommand : Command
    {
        protected CommandHandler(IMediator mediator) : base(mediator)
        {
        }

        public async Task<Unit> Handle(TCommand command, CancellationToken cancellationToken)
        {
            await HandleAsync(command, cancellationToken);
            return Unit.Value;
        }

        protected abstract Task HandleAsync(TCommand command, CancellationToken cancellationToken);
    }
}