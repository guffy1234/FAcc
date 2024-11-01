using FuelAcc.Domain.Entities.Dictionaries;
using System.Text.Json.Serialization;

namespace FuelAcc.Domain.Entities.Documents
{
    public class OrderLine
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        [JsonIgnore]
        public Product Product { get; set; }

        public decimal Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? Sum { get; set; }
    }
}