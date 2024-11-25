namespace FuelAcc.Application.DtoCommon.Paging
{
    public interface IPagedResult<T> : IPagedResultBase
    {
        ICollection<T> Results { get; set; }
    }
}