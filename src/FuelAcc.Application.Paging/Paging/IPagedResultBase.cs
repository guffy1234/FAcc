namespace FuelAcc.Application.DtoCommon.Paging
{
    public interface IPagedResultBase
    {
        int CurrentPage { get; set; }
        int FirstRowOnPage { get; }
        int LastRowOnPage { get; }
        int PageCount { get; set; }
        int PageSize { get; set; }
        int RowCount { get; set; }
    }
}