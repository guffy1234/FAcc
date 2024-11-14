using FuelAcc.Application.Interface.Events;
using FuelAcc.Application.Interface.Persistence;
using FuelAcc.Application.Interface.Replication;
using FuelAcc.Application.UseCases.Replication;

namespace FuelAcc.Application.UseCases.Commons.Events.Handlers
{
    public abstract class EventHandler
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IEventService _eventService;

        protected EventHandler(IUnitOfWork unitOfWork, IEventService eventService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _eventService = eventService ?? throw new ArgumentNullException(nameof(eventService));
        }
    }
}