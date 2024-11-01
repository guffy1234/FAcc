using FuelAcc.Application.Interface.Exceptions;
using FuelAcc.Application.Interface.Persistence;
using MediatR;

namespace FuelAcc.Application.UseCases.Commons.Events.Handlers
{
    public abstract class EventHandler<TEvent> : EventHandler, IRequestHandler<TEvent, Unit> where TEvent : Event
    {
        protected EventHandler(IUnitOfWork unitOfWork, IEventStoreRepository eventStore) : base(unitOfWork, eventStore)
        {
        }

        public async Task<Unit> Handle(TEvent @event, CancellationToken cancellationToken)
        {
            await HandleAsync(@event, cancellationToken);
            return Unit.Value;
        }

        protected abstract Task HandleAsync(TEvent @event, CancellationToken cancellationToken);
    }
}