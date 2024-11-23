namespace FuelAcc.Application.Paging
{
    public class PagedResult<T> : PagedResultBase
    {
        public ICollection<T> Results { get; set; }

        public PagedResult()
        {
            Results = new List<T>();
        }
    }
}