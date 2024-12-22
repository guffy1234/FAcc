using System.ComponentModel.DataAnnotations;

namespace FuelAcc.Application.UseCases.Reports
{
    public sealed class ReportRestsDto
    {
        [Required]
        public bool NonEmptyOnly { get; set; }

        public IReadOnlyCollection<Guid>? StorageId { get; set; }
        public IReadOnlyCollection<Guid>? ProductId { get; set; }
    }
}