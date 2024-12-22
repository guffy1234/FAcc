using FuelAcc.ApiClient;
using FuelAcc.Application.DtoCommon.Paging;

namespace FuelAcc.Client.Services.Crud
{
    // helper need to convert exact paged DTO produced by NSWAG to generic.
    public static class PagedDtoExtensions
    {
        public static PagedResult<DTO> ToGeneric<DTO>(this IPagedResult<DTO> res) => new PagedResult<DTO>()
        {
            CurrentPage = res.CurrentPage,
            PageSize = res.PageSize,
            PageCount = res.PageCount,
            RowCount = res.RowCount,
            Results = res.Results
        };

        public static PagedResult<ProductDto> ToGeneric(this ProductDtoPagedResult res) => new PagedResult<ProductDto>()
        {
            CurrentPage = res.CurrentPage,
            PageSize = res.PageSize,
            PageCount = res.PageCount,
            RowCount = res.RowCount,
            Results = res.Results
        };

        public static PagedResult<PartnerDto> ToGeneric(this PartnerDtoPagedResult res) => new PagedResult<PartnerDto>()
        {
            CurrentPage = res.CurrentPage,
            PageSize = res.PageSize,
            PageCount = res.PageCount,
            RowCount = res.RowCount,
            Results = res.Results
        };

        public static PagedResult<BranchDto> ToGeneric(this BranchDtoPagedResult res) => new PagedResult<BranchDto>()
        {
            CurrentPage = res.CurrentPage,
            PageSize = res.PageSize,
            PageCount = res.PageCount,
            RowCount = res.RowCount,
            Results = res.Results
        };

        public static PagedResult<StorageDto> ToGeneric(this StorageDtoPagedResult res) => new PagedResult<StorageDto>()
        {
            CurrentPage = res.CurrentPage,
            PageSize = res.PageSize,
            PageCount = res.PageCount,
            RowCount = res.RowCount,
            Results = res.Results
        };

        public static PagedResult<OrderInDto> ToGeneric(this OrderInDtoPagedResult res) => new PagedResult<OrderInDto>()
        {
            CurrentPage = res.CurrentPage,
            PageSize = res.PageSize,
            PageCount = res.PageCount,
            RowCount = res.RowCount,
            Results = res.Results
        };

        public static PagedResult<OrderOutDto> ToGeneric(this OrderOutDtoPagedResult res) => new PagedResult<OrderOutDto>()
        {
            CurrentPage = res.CurrentPage,
            PageSize = res.PageSize,
            PageCount = res.PageCount,
            RowCount = res.RowCount,
            Results = res.Results
        };

        public static PagedResult<OrderMoveDto> ToGeneric(this OrderMoveDtoPagedResult res) => new PagedResult<OrderMoveDto>()
        {
            CurrentPage = res.CurrentPage,
            PageSize = res.PageSize,
            PageCount = res.PageCount,
            RowCount = res.RowCount,
            Results = res.Results
        };
    }
}