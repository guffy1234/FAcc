using FuelAcc.Domain.Commons;
using FuelAcc.Domain.Entities.Documents;
using System.Text.Json.Serialization;

namespace FuelAcc.Domain.Entities.Dictionaries
{
    public class FileBlob : DictionaryBase, IDictionaryWithFolderEntity
    {
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public long Size { get; set; }
        public string SHA256 { get; set; }
        public byte[] Body { get; set; }

        public Guid? FolderId { get; set; }

        [JsonIgnore]
        public Folder? Folder { get; set; }

        [JsonIgnore]
        public ICollection<Product> Products { get; set; }

        [JsonIgnore]
        public ICollection<OrderBase> Orders { get; set; }
    }
}