using System.ComponentModel.DataAnnotations;

namespace FuelAcc.Application.UseCases.Reports
{
    public sealed class ReportTransactionView
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public Guid OrderId { get; set; }

        [Required]
        public string OrderNumber { get; set; }

        public Guid? SrcRestId { get; set; }
        public Guid? SrcStorageId { get; set; }
        public string? SrcStorageName { get; set; }

        public Guid? DstRestId { get; set; }
        public Guid? DstStorageId { get; set; }
        public string? DstStorageName { get; set; }

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