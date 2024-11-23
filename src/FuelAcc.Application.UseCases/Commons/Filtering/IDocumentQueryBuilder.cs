using FuelAcc.Domain.Commons;

namespace FuelAcc.Application.UseCases.Commons.Filtering
{
    public interface IDocumentQueryBuilder<ENTITY> : IEntityQueryBuilder<ENTITY> where ENTITY : class, IDocumentEntity
    {

    }
}