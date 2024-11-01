namespace FuelAcc.Application.UseCases.Commons.Queries
{
    public sealed record GetAllQuery<DTO> : Query<IAsyncEnumerable<DTO>>;
}