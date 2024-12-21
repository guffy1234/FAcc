using System.ComponentModel.DataAnnotations;

namespace FuelAcc.Application.UseCases.Reports
{
    public sealed class ReportRestView
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid StorageId { get; set; }
        [Required]
        public string StorageName { get; set; }

        [Required]
        public Guid BranchId { get; set; }
        [Required]
        public string BranchName { get; set; }

        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }

        [Required]
        public decimal Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}