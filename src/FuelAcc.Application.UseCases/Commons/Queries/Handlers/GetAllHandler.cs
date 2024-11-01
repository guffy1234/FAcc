using AutoMapper;
using FuelAcc.Application.Interface;
using FuelAcc.Application.Interface.Persistence;
using FuelAcc.Domain.Commons;
using MediatR;

namespace FuelAcc.Application.UseCases.Commons.Queries.Handlers
{
    public class GetAllHandler<ENTITY, DTO, APOINT> : IRequestHandler<GetAllQuery<DTO>, IAsyncEnumerable<DTO>>
        where DTO : class
        where ENTITY : class, IRootEntity
        where APOINT : class, IAuthorizationPoint, new()
    {
        private readonly IMapper _mapper;
        private readonly IEntityReadRepository<ENTITY> _repository;
        private readonly IAuthorizationChecker _authorizationChecker;

        public GetAllHandler(IMapper mapper, IEntityReadRepository<ENTITY> repository, IAuthorizationChecker authorizationChecker)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _authorizationChecker = authorizationChecker ?? throw new ArgumentNullException(nameof(authorizationChecker));
        }

        public async Task<IAsyncEnumerable<DTO>> Handle(GetAllQuery<DTO> request, CancellationToken cancellationToken)
        {
            var apoint = new APOINT()
            {
                Action = ApplicationAction.View,
            };
            _authorizationChecker.Authorize(apoint);

            var entities = _repository.GetAllAsync();

            async IAsyncEnumerable<DTO> ConversionEnumerator()
            {
                await foreach (var entity in entities)
                {
                    var dto = _mapper.Map<DTO>(entity);
                    yield return dto;
                }
            }

            var ai = ConversionEnumerator();

            return ai;
        }
    }

}