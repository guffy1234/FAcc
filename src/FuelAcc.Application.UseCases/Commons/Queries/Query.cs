using MediatR;

namespace FuelAcc.Application.UseCases.Commons.Queries
{
    public abstract record Query<T> : IRequest<T>;
}