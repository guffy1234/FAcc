using FuelAcc.Domain.Commons;

namespace FuelAcc.Application.UseCases.Commons.Filtering
{
    public interface IDictionaryQueryBuilder<ENTITY> : IEntityQueryBuilder<ENTITY> 
        where ENTITY : class, IDictionaryEntity
    {
        
    }
}