using FuelAcc.Application.DtoCommon.Dictionaries;
using FuelAcc.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FuelAcc.Application.Dto.Dictionaries
{
    public class ProductDto : IDictionaryDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public ProductCategoryType Category { get; set; }

        [Required]
        public ProductUnitsType Units { get; set; }
    }
}