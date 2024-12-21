using FuelAcc.Application.DtoCommon.Dictionaries;

namespace FuelAcc.Application.Dto.Querying
{
    public class FileBlobQueryDto : DictionaryQueryDto, IDictionaryWithFoldersQueryDto
    {
        public IReadOnlyCollection<Guid>? FolderId { get; set; }
    }
}