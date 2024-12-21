using FuelAcc.Domain.Commons;
using System.Text.Json.Serialization;

namespace FuelAcc.Domain.Entities.Dictionaries
{
    public class Partner : DictionaryBase, IDictionaryWithFolderEntity
    {
        public string? ContactName { get; set; }
        public string? ContactTitle { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }

        public Guid? FolderId { get; set; }

        [JsonIgnore]
        public Folder? Folder { get; set; }
    }
}