namespace FuelAcc.Application.DtoCommon.Paging
{
    public class PagedResult<T> : PagedResultBase, IPagedResult<T>
    {
        public ICollection<T> Results { get; set; }

        public PagedResult()
        {
            Results = new List<T>();
        }
    }
}