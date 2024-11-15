using FuelAcc.Domain.Entities.Documents;
using FuelAcc.Domain.Entities.Registry;
using System.Text.Json.Serialization;

namespace FuelAcc.Domain.Entities.Dictionaries
{
    public class Product : DictionaryBase
    {
        [JsonIgnore]
        public ICollection<Rest>? Rests { get; set; }

        [JsonIgnore]
        public ICollection<Transaction>? Transactions { get; set; }

        [JsonIgnore]
        public ICollection<OrderLine>? OrderLines{ get; set; }
    }
}