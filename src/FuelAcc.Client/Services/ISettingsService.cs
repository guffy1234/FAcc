namespace FuelAcc.Client.Services
{
    public interface ISettingsService
    {
        Task<Guid> GetCurrentBranchId();
    }
}