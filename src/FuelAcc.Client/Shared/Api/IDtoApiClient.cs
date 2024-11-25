namespace FuelAcc.Client.Shared.Api
{
    public interface IDtoApiClient<DTO, PAGES_DTO, QUERY_DTO>
        where DTO : class
        where PAGES_DTO : class
        where QUERY_DTO : class
    {
        Task<ICollection<DTO>> AllAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<PAGES_DTO> QueryAsync(QUERY_DTO body = null, CancellationToken cancellationToken = default(CancellationToken));

        Task<DTO> ReadAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));

        Task InsertAsync(DTO body = null, CancellationToken cancellationToken = default(CancellationToken));

        Task UpdateAsync(DTO body = null, CancellationToken cancellationToken = default(CancellationToken));

        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
    }
}