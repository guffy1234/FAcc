namespace FuelAcc.Application.DtoCommon.Dictionaries
{
    public interface IDictionaryWithFoldersQueryDto
    {
        IReadOnlyCollection<Guid>? FolderId { get; set; }
    }
}