using FuelAcc.Application.Interface.Events;
using FuelAcc.Application.Interface.Replication;
using FuelAcc.Domain.Entities.Other;

namespace FuelAcc.Application.UseCases.Events
{
    public class EventService : IEventService
    {
        protected readonly IEventStoreRepository _eventStore;
        private readonly IEventConverter _eventConverter;

        public EventService(IEventStoreRepository eventStore,
            IEventConverter eventConverter)
        {
            _eventStore = eventStore ?? throw new ArgumentNullException(nameof(eventStore));
            _eventConverter = eventConverter ?? throw new ArgumentNullException(nameof(eventConverter));
        }

        async Task IEventService.PublishEventAsync<ENTITY>(DomainEvent<ENTITY> domainEvent, CancellationToken cancellationToken)
        {
            var persist = _eventConverter.ToPersistEvent(domainEvent);

            await _eventStore.InsertEventAsync(persist, cancellationToken);
        }
    }
}