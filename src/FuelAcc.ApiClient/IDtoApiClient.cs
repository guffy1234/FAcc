namespace FuelAcc.ApiClient
{
    public interface IDtoApiClient<DTO, PAGES_DTO, QUERY_DTO>
        where DTO : class
        where PAGES_DTO : class
        where QUERY_DTO : class
    {
        Task<ICollection<DTO>> AllAsync(CancellationToken cancellationToken = default);

        Task<PAGES_DTO> QueryAsync(QUERY_DTO body = null, CancellationToken cancellationToken = default);

        Task<DTO> ReadAsync(Guid id, CancellationToken cancellationToken = default);

        Task InsertAsync(DTO body = null, CancellationToken cancellationToken = default);

        Task UpdateAsync(DTO body = null, CancellationToken cancellationToken = default);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}