namespace FuelAcc.Application.DtoCommon.Paging
{
    public interface IPagedQueryDto
    {
        int? Page { get; set; }
        int? PageSize { get; set; }
    }
}