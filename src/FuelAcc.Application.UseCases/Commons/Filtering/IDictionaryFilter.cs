using FuelAcc.Domain.Commons;

namespace FuelAcc.Application.UseCases.Commons.Filtering
{
    public interface IDictionaryFilter<ENTITY> : IEntityFilter<ENTITY> 
        where ENTITY : class, IDictionaryEntity
    {

    }
}