using FuelAcc.Application.DtoCommon.Paging;

namespace FuelAcc.Application.Dto.Querying
{
    public class PagedQueryDto : IPagedQueryDto
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }
}