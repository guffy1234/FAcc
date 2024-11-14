using AutoMapper;
using FuelAcc.Application.Interface.Events;
using FuelAcc.Application.Interface.Persistence;
using FuelAcc.Application.Interface.Replication;
using FuelAcc.Domain.Commons;
using FuelAcc.Domain.Entities;
using FuelAcc.Domain.Entities.Dictionaries;

namespace FuelAcc.Application.UseCases.Commons.Events.Handlers
{
    public class DeleteEventHandler<ENTITY> : EventHandler<DeleteEvent<ENTITY>>
        where ENTITY : class, IRootEntity
    {
        protected readonly IMapper _mapper;
        protected readonly IEntityWriteRepository<ENTITY> _repository;

        public DeleteEventHandler(IUnitOfWork unitOfWork,
            IEventService eventService, 
            IEntityWriteRepository<ENTITY> repository, 
            IMapper mapper) :
            base(unitOfWork, eventService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        protected virtual bool NeedTransaction => false;

        protected virtual async Task AdditionalProcessing(Guid entityId, CancellationToken cancellationToken)
        {
        }

        protected override async Task HandleAsync(DeleteEvent<ENTITY> @event, CancellationToken cancellationToken)
        {
            var domainEvent = @event.DomainEvent;

            var entity = domainEvent.Entity;

            if (NeedTransaction && !@event.IsInRepliactionContext)
            {
                await _unitOfWork.BeginTransactionAsync(cancellationToken);
            }

            if (entity is ISoftDeleted softDeleted)
            {
                if (entity is ChangesInfo info)
                {
                    info.ModifierUserId = domainEvent.UserId;
                    info.Modified = domainEvent.Date;
                }

                softDeleted.IsDeleted = true;
                await _repository.UpdateAsync(entity, cancellationToken);
            }
            else
            {
                await _repository.DeleteAsync(domainEvent.EntityId, cancellationToken);
            }

            await AdditionalProcessing(domainEvent.EntityId, cancellationToken);

            await _eventService.PublishEventAsync(domainEvent, cancellationToken);

            if (!@event.IsInRepliactionContext)
            {
                await _unitOfWork.SaveAsync(cancellationToken);
            }
        }
    }
}