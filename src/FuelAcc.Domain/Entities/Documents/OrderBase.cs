using FuelAcc.Domain.Commons;
using FuelAcc.Domain.Entities.Dictionaries;
using System.Text.Json.Serialization;

namespace FuelAcc.Domain.Entities.Documents
{
    public abstract class OrderBase : ChangesInfo, IDocumentEntity
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string Number { get; set; }
        public string Description { get; set; }
        public decimal Total { get; set; }

        public ICollection<OrderLine> Lines { get; set; }
        public ICollection<OrderPropertyLine> Properties { get; set; }

        [JsonIgnore]
        public ICollection<FileBlob>? Blobs { get; set; }
    }
}