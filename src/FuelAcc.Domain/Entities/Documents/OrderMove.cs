using FuelAcc.Domain.Entities.Dictionaries;
using FuelAcc.Domain.Enums;
using System.Text.Json.Serialization;

namespace FuelAcc.Domain.Entities.Documents
{
    public sealed class OrderMove : OrderBase
    {
        public Guid? FromStorageId { get; set; }

        [JsonIgnore]
        public Storage? FromStorage { get; set; }

        public Guid? ToStorageId { get; set; }

        [JsonIgnore]
        public Storage? ToStorage { get; set; }

        public OrderMoveType MoveType { get; set; }
    }
}