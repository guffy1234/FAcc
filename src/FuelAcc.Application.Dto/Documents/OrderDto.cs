using FuelAcc.Application.DtoCommon.Documents;
using System.ComponentModel.DataAnnotations;

namespace FuelAcc.Application.Dto.Documents
{
    public class OrderDto : IDocumentDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public IReadOnlyCollection<OrderLineDto> Lines { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Total { get; set; }

        public IReadOnlyCollection<OrderPropertyDto>? Properties { get; set; }
    }
}