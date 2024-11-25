using FuelAcc.Application.DtoCommon.Dictionaries;

namespace FuelAcc.Application.Dto.Dictionaries
{
    public class StorageDto : IDictionaryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid BranchId { get; set; }
    }
}