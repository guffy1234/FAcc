using FuelAcc.Domain.Entities.Registry;
using System.Text.Json.Serialization;

namespace FuelAcc.Domain.Entities.Dictionaries
{
    public class Storage : DictionaryBase
    {
        public Guid BranchId { get; set; }
        public Branch Branch { get; set; }

        [JsonIgnore]
        public ICollection<Rest>? Rests { get; set; }
    }
}