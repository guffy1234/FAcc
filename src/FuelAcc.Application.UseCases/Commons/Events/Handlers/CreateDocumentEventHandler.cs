using AutoMapper;
using FuelAcc.Application.Interface.Accounting;
using FuelAcc.Application.Interface.Exceptions;
using FuelAcc.Application.Interface.Persistence;
using FuelAcc.Domain.Commons;

namespace FuelAcc.Application.UseCases.Commons.Events.Handlers
{
    public class CreateDocumentEventHandler<ENTITY> : CreateEventHandler<ENTITY>
        where ENTITY : class, IDocumentEntity
    {
        private readonly IDocumentTransactionsProcessor<ENTITY> _transactionsProcessor;

        public CreateDocumentEventHandler(IUnitOfWork unitOfWork,
            IEventStoreRepository eventStore,
            IEntityWriteRepository<ENTITY> repository,
            IDocumentTransactionsProcessor<ENTITY> transactionsProcessor,
            IMapper mapper) :
            base(unitOfWork, eventStore, repository, mapper)
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