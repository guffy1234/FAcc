using FuelAcc.Domain.Commons;
using FuelAcc.Domain.Enums;

namespace FuelAcc.Application.UseCases.Authorization
{
    internal class AuthorizationPointTmpl<ENTITY> : AuthorizationPoint
        where ENTITY : class, IRootEntity
    {
        public AuthorizationPointTmpl(ApplicationArea area) : base(area)
        {
            ObjectName = typeof(ENTITY).Name;
        }
    }
}