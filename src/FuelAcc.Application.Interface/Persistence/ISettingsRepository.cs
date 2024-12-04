using FuelAcc.Domain.Entities.Other;

namespace FuelAcc.Application.Interface.Persistence;

public interface ISettingsRepository
{
    Task<Settings?> GetAsync(CancellationToken cancellationToken);
    Task UpsertAsync(Settings entity, CancellationToken cancellationToken);
}
