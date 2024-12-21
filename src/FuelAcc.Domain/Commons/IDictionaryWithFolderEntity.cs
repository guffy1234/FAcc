namespace FuelAcc.Domain.Commons
{
    public interface IDictionaryWithFolderEntity : IDictionaryEntity
    {
        public Guid? FolderId { get; set; }
    }
}