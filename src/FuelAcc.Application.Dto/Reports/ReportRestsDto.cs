using FuelAcc.Domain;

namespace FuelAcc.Application.UseCases.Reports
{
    public sealed class ReportRestsDto
    {
        public List<Guid>? StorageId { get; set; }
        public List<Guid>? ProductId { get; set; }
    }

}