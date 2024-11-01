using FuelAcc.Domain.Entities.Dictionaries;
using System.Text.Json.Serialization;

namespace FuelAcc.Domain.Entities.Documents
{
    public sealed class OrderOut : OrderBase
    {
        public Guid PartnerId { get; set; }

        [JsonIgnore]
        public Partner Partner { get; set; }

        public Guid FromStorageId { get; set; }

        [JsonIgnore]
        public Storage FromStorage { get; set; }
    }
}