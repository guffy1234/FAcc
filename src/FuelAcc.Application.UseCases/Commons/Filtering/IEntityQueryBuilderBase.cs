namespace FuelAcc.Application.UseCases.Commons.Filtering
{
    public interface IEntityQueryBuilderBase
    {
        int? Page { get; }
        int? PageSize { get; }
    }
}