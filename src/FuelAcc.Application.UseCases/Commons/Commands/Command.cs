using MediatR;

namespace FuelAcc.Application.UseCases.Commons.Commands
{
    public abstract record Command : IRequest<Unit>;
}