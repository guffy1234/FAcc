using AutoMapper;
using FuelAcc.Application.Interface;
using FuelAcc.Application.Interface.Persistence;
using FuelAcc.Application.UseCases.Authorization;
using MediatR;

namespace FuelAcc.Application.UseCases.Reports.Transactions
{
    public class ReportTransactionsHandler : IRequestHandler<ReportTransactionsQuery, IAsyncEnumerable<ReportTransactionView>>
    {
        private readonly IMapper _mapper;
        private readonly IReportsRepository _repository;
        private readonly IAuthorizationChecker _authorizationChecker;

        public ReportTransactionsHandler(IMapper mapper, IReportsRepository repository, IAuthorizationChecker authorizationChecker)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _authorizationChecker = authorizationChecker ?? throw new ArgumentNullException(nameof(authorizationChecker));
        }

        public async Task<IAsyncEnumerable<ReportTransactionView>> Handle(ReportTransactionsQuery request, CancellationToken cancellationToken)
        {
            var apoint = new ReportAuthorizationPoint()
            {
                ObjectName = "Transactions",
                Action = ApplicationAction.View,
            };
            _authorizationChecker.Authorize(apoint);

            var dto = request.dto;

            var entities = _repository.GetTransactions(dto.From, dto.To, dto.OrderId, dto.SourceId, dto.DestinationId, dto.ProductId);

            async IAsyncEnumerable<ReportTransactionView> ConversionEnumerator()
            {
                await foreach (var entity in entities)
                {
                    var view = _mapper.Map<ReportTransactionView>(entity);
                    yield return view;
                }
            }

            var ai = ConversionEnumerator();

            return ai;
        }
    }
}