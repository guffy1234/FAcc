using System.Text.Json.Serialization;

namespace FuelAcc.Domain.Entities.Dictionaries
{
    public class Branch : DictionaryBase
    {
        [JsonIgnore]
        public ICollection<Storage>? Storages { get; set; }
    }
}