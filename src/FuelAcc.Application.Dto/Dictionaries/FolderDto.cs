using FuelAcc.Application.DtoCommon.Dictionaries;
using System.ComponentModel.DataAnnotations;

namespace FuelAcc.Application.Dto.Dictionaries
{
    public class FolderDto : IDictionaryDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Guid? ParentId { get; set; }
    }
}