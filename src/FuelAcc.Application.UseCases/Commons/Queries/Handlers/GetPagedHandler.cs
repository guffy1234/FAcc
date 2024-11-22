using AutoMapper;
using FuelAcc.Application.Dto;
using FuelAcc.Application.Interface;
using FuelAcc.Application.Interface.Persistence;
using FuelAcc.Application.UseCases.Commons.Filtering;
using FuelAcc.Domain.Commons;
using FuelAcc.Domain.Entities.Other;
using MediatR;

namespace FuelAcc.Application.UseCases.Commons.Queries.Handlers
{
    public class GetPagedHandler<ENTITY, DTO, APOINT> : IRequestHandler<GetPaged<DTO>, PagedResult<DTO>>
        where DTO : class
        where ENTITY : class, IRootEntity
        where APOINT : class, IAuthorizationPoint, new()
    {
        private readonly IMapper _mapper;
        private readonly IEntityReadRepository<ENTITY> _repository;
        private readonly IAuthorizationChecker _authorizationChecker;

        public GetPagedHandler(IMapper mapper, IEntityReadRepository<ENTITY> repository, IAuthorizationChecker authorizationChecker)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _authorizationChecker = authorizationChecker ?? throw new ArgumentNullException(nameof(authorizationChecker));
        }

        public async Task<PagedResult<DTO>> Handle(GetPaged<DTO> request, CancellationToken cancellationToken)
        {
            var apoint = new APOINT()
            {
                Action = ApplicationAction.View,
            };
            _authorizationChecker.Authorize(apoint);

            var filter = request.Filter as IEntityFilter<ENTITY>;

            if (filter == null)
            {
                var entityType = typeof(ENTITY);

                if (entityType.IsAssignableTo(typeof(IDictionaryEntity)))
                {
                    var genericType = typeof(DefaultDictionaryFilter<>);
                    var appliedType = genericType.MakeGenericType(entityType);
                    var instance = Activator.CreateInstance(appliedType!);

                    filter = instance as IEntityFilter<ENTITY>;
                } else if (entityType.IsAssignableTo(typeof(IDocumentEntity)))
                {
                    var genericType = typeof(DefaultDocumentFilter<>);
                    var appliedType = genericType.MakeGenericType(entityType);
                    var instance = Activator.CreateInstance(appliedType!);

                    filter = instance as IEntityFilter<ENTITY>;
                }
            }

            var fetched = await _repository.GetExtendedAsync(query => { 
                if(filter != null)
                {
                    query = filter.BuildFilterAndSort(query);
                }
                return query;
            }, request.Page, request.PageSize, true, cancellationToken);

            var result = new PagedResult<DTO>();
            result.CurrentPage = request.Page;
            result.PageSize = request.PageSize;

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