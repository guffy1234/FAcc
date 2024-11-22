using FuelAcc.Domain.Commons;

namespace FuelAcc.Application.UseCases.Commons.Filtering
{
    public interface IDocumentFilter<ENTITY> : IEntityFilter<ENTITY> where ENTITY : class, IDocumentEntity
    {

    }
}