using AutoMapper;
using FuelAcc.Application.Interface.Events;
using FuelAcc.Application.Interface.Persistence;
using FuelAcc.Application.Interface.Replication;
using FuelAcc.Domain.Commons;
using FuelAcc.Domain.Entities;
using FuelAcc.Domain.Entities.Dictionaries;

namespace FuelAcc.Application.UseCases.Commons.Events.Handlers
{
    public class CreateEventHandler<ENTITY> : EventHandler<CreateEvent<ENTITY>>
        where ENTITY : class, IRootEntity
    {
        protected readonly IMapper _mapper;
        protected readonly IEntityWriteRepository<ENTITY> _repository;

        public CreateEventHandler(IUnitOfWork unitOfWork,
            IEventService eventService, 
            IEntityWriteRepository<ENTITY> repository, 
            IMapper mapper) :
            base(unitOfWork, eventService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));           
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        protected virtual bool NeedTransaction => false;

        protected virtual async Task AdditionalProcessing(ENTITY enitty, CancellationToken cancellationToken)
        {
        }

        protected override async Task HandleAsync(CreateEvent<ENTITY> @event, CancellationToken cancellationToken)
        {
            var domainEvent = @event.DomainEvent;

            var entity = domainEvent.Entity;
            if (entity is ChangesInfo info)
            {
                info.CreatorUserId = domainEvent.UserId;
                info.Created = domainEvent.Date;
            }

            if (NeedTransaction && !@event.IsInRepliactionContext)
            {
                await _unitOfWork.BeginTransactionAsync(cancellationToken);
            }

            await _repository.InsertAsync(entity, cancellationToken);
            // update ID after Add to db context. if guid was default - here it assigned to new value
            domainEvent.EntityId = entity.Id;

            await AdditionalProcessing(entity, cancellationToken);

            await _eventService.PublishEventAsync(domainEvent, cancellationToken);

            if (!@event.IsInRepliactionContext)
            {
                await _unitOfWork.SaveAsync(cancellationToken);
            }
        }
    }
}