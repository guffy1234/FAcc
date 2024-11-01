using FuelAcc.Application.Interface.Accounting;
using FuelAcc.Domain.Entities.Documents;

namespace FuelAcc.Application.UseCases.Documents.OrdersIn
{
    public class OrderInTransactionsProcessor : IDocumentTransactionsProcessor<OrderIn>
    {
        private readonly ITransactionsService _transactionsService;

        public OrderInTransactionsProcessor(ITransactionsService transactionsService)
        {
            _transactionsService = transactionsService ?? throw new ArgumentNullException(nameof(transactionsService));
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _transactionsService.DeleteAsync(id, cancellationToken);
        }

        public async Task InsertAsync(OrderIn entity, CancellationToken cancellationToken)
        {
            await _transactionsService.InsertAsync(entity.Id, entity.Date, null, entity.ToStorageId, entity.Lines, cancellationToken);
        }

        public async Task UpdateAsync(OrderIn entity, CancellationToken cancellationToken)
        {
            await _transactionsService.UpdateAsync(entity.Id, entity.Date, null, entity.ToStorageId, entity.Lines, cancellationToken);
        }
    }
}