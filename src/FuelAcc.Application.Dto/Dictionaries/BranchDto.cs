using FuelAcc.Application.DtoCommon.Dictionaries;

namespace FuelAcc.Application.Dto.Dictionaries
{
    public class BranchDto : IDictionaryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}