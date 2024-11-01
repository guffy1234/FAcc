using FuelAcc.Application.Interface;
using FuelAcc.Domain.Enums;

namespace FuelAcc.Application.UseCases.Authorization
{
    internal class AuthorizationPoint : IAuthorizationPoint
    {
        public AuthorizationPoint(ApplicationArea area)
        {
            Area = area;
        }

        public ApplicationArea Area { get; init; }
        public string ObjectName { get; init; }
        public ApplicationAction Action { get; init; }
    }
}