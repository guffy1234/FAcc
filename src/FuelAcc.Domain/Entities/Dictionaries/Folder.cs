using System.Text.Json.Serialization;

namespace FuelAcc.Domain.Entities.Dictionaries
{
    public class Folder : DictionaryBase
    {
        public Guid? ParentId { get; set; }

        [JsonIgnore]
        public ICollection<Partner>? Partners { get; set; }

        [JsonIgnore]
        public ICollection<Product>? Products { get; set; }

        [JsonIgnore]
        public ICollection<FileBlob>? FileBlobs { get; set; }
    }
}