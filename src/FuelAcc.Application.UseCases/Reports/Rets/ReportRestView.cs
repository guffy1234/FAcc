using FuelAcc.Application.UseCases.Reports.Transactions;

namespace FuelAcc.Application.UseCases.Reports.Rets
{
    public sealed class ReportRestView
    {
        public Guid Id { get; set; }
        public Guid StorageId { get; set; }
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
    }
}