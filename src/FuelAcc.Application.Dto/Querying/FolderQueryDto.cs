namespace FuelAcc.Application.Dto.Querying
{
    public class FolderQueryDto : DictionaryQueryDto
    {
        public Guid? ParentId { get; set; }

        // when you need IDs of entire subtree for some report you may pass true here
        // later to save bandwith you may implement dedictaed query to fetch only IDs of folders without Names
        public bool? ReturnAllDeepChildren { get; set; }
    }
}