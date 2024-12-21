using AutoMapper;
using FuelAcc.Application.Dto.Accounting;
using FuelAcc.Application.Interface;
using FuelAcc.Application.Interface.Accounting;
using MediatR;

namespace FuelAcc.Application.UseCases.Accounting.Handlers
{
    public class GetAvailableRestsHandler : IRequestHandler<GetAvailableRestsQuery, IEnumerable<AvailableRestView>>
    {
        private readonly IMapper _mapper;
        private readonly ITransactionsService _repository;
        private readonly IAuthorizationChecker _authorizationChecker;

        public GetAvailableRestsHandler(IMapper mapper, ITransactionsService repository, IAuthorizationChecker authorizationChecker)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _authorizationChecker = authorizationChecker ?? throw new ArgumentNullException(nameof(authorizationChecker));
        }

        public async Task<IEnumerable<AvailableRestView>> Handle(GetAvailableRestsQuery request, CancellationToken cancellationToken)
        {
            //todo: implement permissions cheking

            //var apoint = new APOINT()
            //{
            //    Action = ApplicationAction.View,
            //};
            //_authorizationChecker.Authorize(apoint);

            var entities = await _repository.GetAvailableRestsAsync(request.Dto.StorageId, request.Dto.ProductId, cancellationToken);
            var dtos = _mapper.Map<IEnumerable<AvailableRestView>>(entities);
            return dtos;
        }
    }
}