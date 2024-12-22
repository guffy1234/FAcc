using FuelAcc.Application.DtoCommon.Dictionaries;
using System.ComponentModel.DataAnnotations;

namespace FuelAcc.Application.Dto.Dictionaries
{
    public class StorageDto : IDictionaryDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Guid BranchId { get; set; }
    }
}