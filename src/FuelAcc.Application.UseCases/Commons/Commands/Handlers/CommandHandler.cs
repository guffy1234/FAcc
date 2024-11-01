using MediatR;

namespace FuelAcc.Application.UseCases.Commons.Commands.Handlers
{
    public abstract class CommandHandler
    {
        protected readonly IMediator _mediator;

        protected CommandHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
    }
}