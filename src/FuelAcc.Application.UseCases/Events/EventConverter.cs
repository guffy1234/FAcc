using FuelAcc.Application.Interface.Events;
using FuelAcc.Application.UseCases.Commons.Events;
using FuelAcc.Domain.Entities.Dictionaries;
using FuelAcc.Domain.Entities.Other;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace FuelAcc.Application.UseCases.Events
{
    public class EventConverter : IEventConverter
    {
        public object? ToMediatorEvent(PersistEvent persistEvent)
        {
            var entityType = Type.GetType(persistEvent.ObjectClass);

            var entity = JsonSerializer.Deserialize(persistEvent.ObjectJson, entityType);

            var genericDomainEvent = typeof(DomainEvent<>);
            var constructedDomainEvent = genericDomainEvent.MakeGenericType(entityType);
            // todo: handle errors better
            var createdDomainEvent = Activator.CreateInstance(constructedDomainEvent!);

            var domainEvent = createdDomainEvent as DomainEventBase;
            
            domainEvent.Id = persistEvent.Id;
            domainEvent.UserId = persistEvent.UserId;
            domainEvent.BranchId = persistEvent.BranchId;
            domainEvent.Date = persistEvent.Date;
            domainEvent.EventArea = persistEvent.EventArea;
            domainEvent.EventAction = persistEvent.EventAction;
            domainEvent.EntityObject = entity;

            var genericEventType = domainEvent.EventAction switch
            {
                Domain.Enums.EventAction.Insert => typeof(CreateEvent<>),
                Domain.Enums.EventAction.Update => typeof(UpdateEvent<>),
                Domain.Enums.EventAction.Delete => typeof(DeleteEvent<>),
                _ => throw new InvalidOperationException("Unknown action type")
            };

            var constructedEventType = genericEventType.MakeGenericType(entityType);
            var created = Activator.CreateInstance(constructedEventType, new object[] { domainEvent, true });

            return created;

        }

        PersistEvent  IEventConverter.ToPersistEvent<ENTITY>(DomainEvent<ENTITY> domainEvent)
        {
            var etype = typeof(ENTITY);
            var className = etype.AssemblyQualifiedName;

            var persistEvent = new PersistEvent()
            {
                Date = domainEvent.Date,
                BranchId = domainEvent.BranchId,
                UserId = domainEvent.UserId,
                EntityId = domainEvent.EntityId,
                EventAction = domainEvent.EventAction,
                EventArea = domainEvent.EventArea,
                ObjectClass = className,
            };

            // for delete events we haven't entity body
            if (domainEvent.Entity is not null)
            {
                var jsonString = JsonSerializer.Serialize(domainEvent.Entity);
                persistEvent.ObjectJson = jsonString;
            }

            return persistEvent;
        }
    }
}