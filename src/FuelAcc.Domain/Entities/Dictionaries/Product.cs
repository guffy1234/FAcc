using FuelAcc.Domain.Commons;
using FuelAcc.Domain.Entities.Documents;
using FuelAcc.Domain.Entities.Registry;
using FuelAcc.Domain.Enums;
using System.Text.Json.Serialization;

namespace FuelAcc.Domain.Entities.Dictionaries
{
    public class Product : DictionaryBase, IDictionaryWithFolderEntity
    {
        public ProductCategoryType Category { get; set; }
        public ProductUnitsType Units { get; set; }

        public Guid? FolderId { get; set; }
        [JsonIgnore]
        public Folder? Folder { get; set; }

        [JsonIgnore]
        public ICollection<Rest>? Rests { get; set; }

        [JsonIgnore]
        public ICollection<Transaction>? Transactions { get; set; }

        [JsonIgnore]
        public ICollection<OrderLine>? OrderLines{ get; set; }

        [JsonIgnore]
        public ICollection<FileBlob>? Blobs { get; set; }
    }
}