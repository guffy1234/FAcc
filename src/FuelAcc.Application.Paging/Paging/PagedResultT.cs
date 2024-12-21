using System.ComponentModel.DataAnnotations;

namespace FuelAcc.Application.DtoCommon.Paging
{
    public class PagedResult<T> : PagedResultBase, IPagedResult<T>
    {
        [Required]
        public ICollection<T> Results { get; set; }

        public PagedResult()
        {
            Results = new List<T>();
        }
    }
}