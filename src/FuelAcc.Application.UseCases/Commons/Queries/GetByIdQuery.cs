namespace FuelAcc.Application.UseCases.Commons.Queries
{
    public sealed record GetByIdQuery<DTO>(Guid Id) : Query<DTO>;
}