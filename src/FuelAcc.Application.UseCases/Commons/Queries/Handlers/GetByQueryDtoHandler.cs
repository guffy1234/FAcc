using AutoMapper;
using FuelAcc.Application.Dto;
using FuelAcc.Application.Dto.Querying;
using FuelAcc.Application.DtoCommon.Paging;
using FuelAcc.Application.Interface;
using FuelAcc.Application.Interface.Persistence;
using FuelAcc.Application.UseCases.Commons.Filtering;
using FuelAcc.Application.UseCases.Commons.Filtering.Handlers;
using FuelAcc.Domain.Commons;
using FuelAcc.Domain.Entities.Other;
using MediatR;

namespace FuelAcc.Application.UseCases.Commons.Queries.Handlers
{
    public class GetByQueryDtoHandler<ENTITY, DTO, QUERY_DTO, APOINT> : IRequestHandler<GetPagedByQueryDto<DTO, QUERY_DTO>, PagedResult<DTO>>
        where DTO : class
        where QUERY_DTO : PagedQueryDto
        where ENTITY : class, IRootEntity
        where APOINT : class, IAuthorizationPoint, new()
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IEntityReadRepository<ENTITY> _repository;
        private readonly IAuthorizationChecker _authorizationChecker;

        public GetByQueryDtoHandler(IMapper mapper, IMediator mediator, IEntityReadRepository<ENTITY> repository, IAuthorizationChecker authorizationChecker)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _authorizationChecker = authorizationChecker ?? throw new ArgumentNullException(nameof(authorizationChecker));
        }

        public async Task<PagedResult<DTO>> Handle(GetPagedByQueryDto<DTO, QUERY_DTO> request, CancellationToken cancellationToken)
        {
            var apoint = new APOINT()
            {
                Action = ApplicationAction.View,
            };
            _authorizationChecker.Authorize(apoint);

            var queryBuilderBase = await _mediator.Send(new QueryBuilderCommand<QUERY_DTO>(request.dto));

            var filter = queryBuilderBase as IEntityQueryBuilder<ENTITY>;

            var pageIdx = filter?.Page ?? 1;
            var pageSize = filter?.PageSize ?? 0;

            var fetched = await _repository.GetExtendedAsync(query => { 
                if(filter != null)
                {
                    query = filter.Filter(query);
                    query = filter.Sort(query);
                }
                return query;
            }, pageIdx, pageSize, true, cancellationToken);

            var result = new PagedResult<DTO>();
            result.CurrentPage = pageIdx;
            result.PageSize = pageSize;

            result.RowCount = fetched.Total;

            var pageCount = (double)result.RowCount / result.PageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            result.Results = fetched.Items
                .Select(e => _mapper.Map<DTO>(e))
                .ToList();

            return result;
        }
    }
}