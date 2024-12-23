﻿using FuelAcc.Application.DtoCommon.Dictionaries;
using FuelAcc.Application.DtoCommon.Documents;
using FuelAcc.Application.DtoCommon.Paging;

namespace FuelAcc.ApiClient
{
    // these definitions apply known interfaces to classes generated by NSWAg from OpenAPI spec
    // this allows to make implementation more generics based

    public partial interface IBranchesApiClient : IDtoApiClient<BranchDto, BranchDtoPagedResult, BranchQueryDto>
    {
    }

    public partial class BranchDto : IDictionaryDto
    {
    }

    public partial class BranchQueryDto : IDictionaryQueryDto
    {
    }

    public partial class BranchDtoPagedResult : IPagedResult<BranchDto>
    {
    }

    public partial interface IProductsApiClient : IDtoApiClient<ProductDto, ProductDtoPagedResult, ProductQueryDto>
    {
    }

    public partial class ProductDto : IDictionaryDto
    {
    }

    public partial class ProductQueryDto : IDictionaryQueryDto
    {
    }

    public partial class ProductDtoPagedResult : IPagedResult<ProductDto>
    {
    }

    public partial interface IPartnersApiClient : IDtoApiClient<PartnerDto, PartnerDtoPagedResult, PartnerQueryDto>
    {
    }

    public partial class PartnerDto : IDictionaryDto
    {
    }

    public partial class PartnerQueryDto : IDictionaryQueryDto
    {
    }

    public partial class PartnerDtoPagedResult : IPagedResult<PartnerDto>
    {
    }

    public partial interface IStoragesApiClient : IDtoApiClient<StorageDto, StorageDtoPagedResult, StorageQueryDto>
    {
    }

    public partial class StorageDto : IDictionaryDto
    {
    }

    public partial class StorageQueryDto : IDictionaryQueryDto
    {
    }

    public partial class StorageDtoPagedResult : IPagedResult<StorageDto>
    {
    }

    public partial interface IFoldersApiClient : IDtoApiClient<FolderDto, FolderDtoPagedResult, FolderQueryDto>
    {
    }

    public partial class FolderDto : IDictionaryDto
    {
    }

    public partial class FolderQueryDto : IDictionaryQueryDto
    {
    }

    public partial class FolderDtoPagedResult : IPagedResult<FolderDto>
    {
    }

    public partial interface IFileBlobsApiClient : IDtoApiClient<FileBlobDto, FileBlobDtoPagedResult, FileBlobQueryDto>
    {
    }

    public partial class FileBlobDto : IDictionaryDto
    {
    }

    public partial class FileBlobQueryDto : IDictionaryQueryDto
    {
    }

    public partial class FileBlobDtoPagedResult : IPagedResult<FileBlobDto>
    {
    }

    public partial interface IOrdersInApiClient : IDtoApiClient<OrderInDto, OrderInDtoPagedResult, OrderInQueryDto>
    {
    }

    public partial class OrderInDto : IDocumentDto
    {
    }

    public partial class OrderInQueryDto : IDocumentQueryDto
    {
    }

    public partial class OrderInDtoPagedResult : IPagedResult<OrderInDto>
    {
    }

    public partial interface IOrdersOutApiClient : IDtoApiClient<OrderOutDto, OrderOutDtoPagedResult, OrderOutQueryDto>
    {
    }

    public partial class OrderOutDto : IDocumentDto
    {
    }

    public partial class OrderOutQueryDto : IDocumentQueryDto
    {
    }

    public partial class OrderOutDtoPagedResult : IPagedResult<OrderOutDto>
    {
    }

    public partial interface IOrdersMoveApiClient : IDtoApiClient<OrderMoveDto, OrderMoveDtoPagedResult, OrderMoveQueryDto>
    {
    }

    public partial class OrderMoveDto : IDocumentDto
    {
    }

    public partial class OrderMoveQueryDto : IDocumentQueryDto
    {
    }

    public partial class OrderMoveDtoPagedResult : IPagedResult<OrderMoveDto>
    {
    }

    public partial class ReplictionPacketViewDtoPagedResult : IPagedResult<ReplictionPacketViewDto>
    {
    }
}