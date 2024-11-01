using FuelAcc.Application.Interface.Accounting;
using FuelAcc.Domain.Entities.Documents;

namespace FuelAcc.Application.UseCases.Documents.OrdersOut
{
    public class OrderOutTransactionsProcessor : IDocumentTransactionsProcessor<OrderOut>
    {
        private readonly ITransactionsService _transactionsService;

        public OrderOutTransactionsProcessor(ITransactionsService transactionsService)
        {
            _transactionsService = transactionsService ?? throw new ArgumentNullException(nameof(transactionsService));
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _transactionsService.DeleteAsync(id, cancellationToken);
        }

        public async Task InsertAsync(OrderOut entity, CancellationToken cancellationToken)
        {
            await _transactionsService.InsertAsync(entity.Id, entity.Date, entity.FromStorageId, null, entity.Lines, cancellationToken);
        }

        public async Task UpdateAsync(OrderOut entity, CancellationToken cancellationToken)
        {
            await _transactionsService.UpdateAsync(entity.Id, entity.Date, entity.FromStorageId, null, entity.Lines, cancellationToken);
        }
    }
}