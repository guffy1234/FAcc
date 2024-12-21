using System.ComponentModel.DataAnnotations;

namespace FuelAcc.Application.DtoCommon.Paging
{
    public abstract class PagedResultBase : IPagedResultBase
    {
        [Required]
        public int CurrentPage { get; set; }
        [Required]
        public int PageCount { get; set; }
        [Required]
        public int PageSize { get; set; }
        [Required]
        public int RowCount { get; set; }

        [Required]
        public int FirstRowOnPage
        {
            get { return (CurrentPage - 1) * PageSize + 1; }
        }

        [Required]
        public int LastRowOnPage
        {
            get { return Math.Min(CurrentPage * PageSize, RowCount); }
        }
    }
}