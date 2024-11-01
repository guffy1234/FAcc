using FuelAcc.Application.Interface.Accounting;
using FuelAcc.Domain.Entities.Documents;

namespace FuelAcc.Application.UseCases.Documents.OrdersMove
{
    public class OrderMoveTransactionsProcessor : IDocumentTransactionsProcessor<OrderMove>
    {
        private readonly ITransactionsService _transactionsService;

        public OrderMoveTransactionsProcessor(ITransactionsService transactionsService)
        {
            _transactionsService = transactionsService ?? throw new ArgumentNullException(nameof(transactionsService));
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _transactionsService.DeleteAsync(id, cancellationToken);
        }

        public async Task InsertAsync(OrderMove entity, CancellationToken cancellationToken)
        {
            await _transactionsService.InsertAsync(entity.Id, entity.Date, entity.FromStorageId, entity.ToStorageId, entity.Lines, cancellationToken);
        }

        public async Task UpdateAsync(OrderMove entity, CancellationToken cancellationToken)
        {
            await _transactionsService.UpdateAsync(entity.Id, entity.Date, entity.FromStorageId, entity.ToStorageId, entity.Lines, cancellationToken);
        }
    }
}