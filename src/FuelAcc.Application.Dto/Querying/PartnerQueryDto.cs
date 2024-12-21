using FuelAcc.Application.DtoCommon.Dictionaries;

namespace FuelAcc.Application.Dto.Querying
{
    public class PartnerQueryDto : DictionaryQueryDto, IDictionaryWithFoldersQueryDto
    {
        public IReadOnlyCollection<Guid>? FolderId { get; set; }
    }
}