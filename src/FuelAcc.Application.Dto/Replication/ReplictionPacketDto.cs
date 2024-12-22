﻿using System.ComponentModel.DataAnnotations;

namespace FuelAcc.Application.Dto.Replication
{
    public sealed class ReplictionPacketDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public Guid? PreviousId { get; set; }

        [Required]
        public Guid BranchId { get; set; }

        [Required]
        public Guid SourceBranchId { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }

        [Required]
        public ICollection<EventDto> Events { get; set; }

        [Required]
        public bool Outbound { get; set; }
    }
}