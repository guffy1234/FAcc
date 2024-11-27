using AutoMapper;
using FuelAcc.Application.Interface;
using FuelAcc.Application.Interface.Persistence;
using FuelAcc.Application.UseCases.Authorization;
using MediatR;

namespace FuelAcc.Application.UseCases.Reports.Rests
{
    public class ReportRestsHandler : IRequestHandler<ReportRestsQuery, IAsyncEnumerable<ReportRestView>>
    {
        private readonly IMapper _mapper;
        private readonly IReportsRepository _repository;
        private readonly IAuthorizationChecker _authorizationChecker;

        public ReportRestsHandler(IMapper mapper, IReportsRepository repository, IAuthorizationChecker authorizationChecker)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _authorizationChecker = authorizationChecker ?? throw new ArgumentNullException(nameof(authorizationChecker));
        }

        public async Task<IAsyncEnumerable<ReportRestView>> Handle(ReportRestsQuery request, CancellationToken cancellationToken)
        {
            var apoint = new ReportAuthorizationPoint()
            {
                ObjectName = "Rest",
                Action = ApplicationAction.View,
            };
            _authorizationChecker.Authorize(apoint);

            var dto = request.dto;

            var entities = _repository.GetRests(dto.NonEmptyOnly, dto.StorageId, dto.ProductId);

            async IAsyncEnumerable<ReportRestView> ConversionEnumerator()
            {
                await foreach (var entity in entities)
                {
                    var view = _mapper.Map<ReportRestView>(entity);
                    yield return view;
                }
            }

            var ai = ConversionEnumerator();

            return ai;
        }
    }
}