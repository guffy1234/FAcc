using FuelAcc.Application.Interface.Exceptions;
using FuelAcc.Domain.Commons;
using FuelAcc.Domain.Entities.Dictionaries;
using FuelAcc.Domain.Entities.Other;
using FuelAcc.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace FuelAcc.Persistence.Repositories
{
    internal class EventStoreRepository : IEventStoreRepository
    {
        protected readonly AppDbContext _dbContext;
        private Guid _currentBranchId;

        public EventStoreRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InsertEventAsync<ENTITY>(DomainEvent<ENTITY> domainEvent, CancellationToken cancellationToken)
            where ENTITY : class, IRootEntity
        {
            await ReadOrCreateCurrentBranch(domainEvent, cancellationToken);

            var persistEvent = new PersistEvent()
            {
                Date = domainEvent.Date,
                UserId = domainEvent.UserId,
                EntityId = domainEvent.EntityId,
                EventAction = domainEvent.EventAction,
                EventArea = domainEvent.EventArea,
                BranchId = _currentBranchId,
                ObjectClass = typeof(ENTITY).Name,
            };

            // for delete events we haven't entity body
            if (domainEvent.Entity is not null)
            {
                var jsonString = JsonSerializer.Serialize(domainEvent.Entity);
                persistEvent.ObjectJson = jsonString;
            }

            await _dbContext.AddAsync(persistEvent, cancellationToken);
        }

        private async Task ReadOrCreateCurrentBranch<ENTITY>(DomainEvent<ENTITY> domainEvent, CancellationToken cancellationToken)
            where ENTITY : class, IRootEntity
        {
            if (_currentBranchId == Guid.Empty)
            {
                var settings = await _dbContext.Settings.FirstOrDefaultAsync(cancellationToken);
                if (settings != null)
                {
                    _currentBranchId = settings.BranchId;
                }
                else
                {
                    var entity = domainEvent.Entity;
                    if (domainEvent.EventAction == Domain.Enums.EventAction.Insert && entity != null && entity is Branch branch)
                    {
                        settings = new Settings
                        {
                            BranchId = branch.Id,
                        };
                        await _dbContext.AddAsync(settings, cancellationToken);

                        _currentBranchId = settings.BranchId;
                    }
                    else
                    {
                        throw new InvalidOperationException("You can't save any entity until you create first Branch and save it to Settings");
                    }
                }
            }
        }
    }
}