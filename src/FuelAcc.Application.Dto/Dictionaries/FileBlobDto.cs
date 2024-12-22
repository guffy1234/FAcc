using FuelAcc.Application.DtoCommon.Dictionaries;
using System.ComponentModel.DataAnnotations;

namespace FuelAcc.Application.Dto.Dictionaries
{
    public class FileBlobDto : IDictionaryDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string MimeType { get; set; }

        [Required]
        public long Size { get; set; }

        [Required]
        public string SHA256 { get; set; }

        public byte[] Body { get; set; }
    }
}