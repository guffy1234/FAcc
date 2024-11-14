﻿using AutoMapper;
using FuelAcc.Application.Interface;
using FuelAcc.Application.Interface.Accounting;
using FuelAcc.Application.Interface.Events;
using FuelAcc.Application.Interface.Persistence;
using FuelAcc.Application.Interface.Replication;
using FuelAcc.Domain.Commons;

namespace FuelAcc.Application.UseCases.Commons.Events.Handlers
{
    public class CreateDocumentEventHandler<ENTITY> : CreateEventHandler<ENTITY>
        where ENTITY : class, IDocumentEntity
    {
        private readonly IDocumentTransactionsProcessor<ENTITY> _transactionsProcessor;

        public CreateDocumentEventHandler(IUnitOfWork unitOfWork,
            IEventService eventService,
            IExecutionContext executionContext,
            IEntityWriteRepository<ENTITY> repository,
            IDocumentTransactionsProcessor<ENTITY> transactionsProcessor,
            IMapper mapper) :
            base(unitOfWork, eventService, executionContext, repository, mapper)
        {
            _transactionsProcessor = transactionsProcessor ?? throw new ArgumentNullException(nameof(transactionsProcessor));
        }

        protected override bool NeedTransaction => true;

        protected override async Task AdditionalProcessing(ENTITY entity, CancellationToken cancellationToken)
        {
            await _transactionsProcessor.InsertAsync(entity, cancellationToken);
        }
    }
}