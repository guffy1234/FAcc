using FuelAcc.Domain.Entities.Dictionaries;
using System.Text.Json.Serialization;

namespace FuelAcc.Domain.Entities.Documents
{
    public sealed class OrderIn : OrderBase
    {
        public Guid PartnerId { get; set; }

        [JsonIgnore]
        public Partner Partner { get; set; }

        public Guid ToStorageId { get; set; }

        [JsonIgnore]
        public Storage ToStorage { get; set; }
    }
}