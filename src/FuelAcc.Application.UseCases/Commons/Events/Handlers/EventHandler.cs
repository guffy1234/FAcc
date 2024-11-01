using FuelAcc.Application.Interface.Exceptions;
using FuelAcc.Application.Interface.Persistence;

namespace FuelAcc.Application.UseCases.Commons.Events.Handlers
{
    public abstract class EventHandler
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IEventStoreRepository _eventStore;

        protected EventHandler(IUnitOfWork unitOfWork, IEventStoreRepository eventStore)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _eventStore = eventStore ?? throw new ArgumentNullException(nameof(eventStore));
        }
    }
}