using FuelAcc.Application.DtoCommon.Dictionaries;
using System.ComponentModel.DataAnnotations;

namespace FuelAcc.Application.Dto.Dictionaries
{
    public class PartnerDto : IDictionaryDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? ContactName { get; set; }
        public string? ContactTitle { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
    }
}