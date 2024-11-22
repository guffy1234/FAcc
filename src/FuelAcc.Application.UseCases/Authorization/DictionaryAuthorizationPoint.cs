using FuelAcc.Domain.Commons;
using FuelAcc.Domain.Enums;

namespace FuelAcc.Application.UseCases.Authorization
{
    internal class DictionaryAuthorizationPoint<ENTITY> : AuthorizationPointTmpl<ENTITY>
        where ENTITY : class, IDictionaryEntity
    {
        public DictionaryAuthorizationPoint() : base(ApplicationArea.Dictionary)
        {
        }
    }
}