﻿namespace FuelAcc.Application.UseCases.Reports
{
    public sealed class ReportRestView
    {
        public Guid Id { get; set; }

        public Guid StorageId { get; set; }
        public string StorageName { get; set; }

        public Guid BranchId { get; set; }
        public string BranchName { get; set; }

        public Guid ProductId { get; set; }
        public string ProductName { get; set; }

        public decimal Quantity { get; set; }
    }
}