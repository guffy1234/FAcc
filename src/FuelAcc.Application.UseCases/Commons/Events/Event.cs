using MediatR;

namespace FuelAcc.Application.UseCases.Commons.Events
{
    public abstract record Event : IRequest<Unit>;
}