using FuelAcc.Domain.Enums;

namespace FuelAcc.Application.Interface
{
    public interface IAuthorizationPoint
    {
        public ApplicationArea Area { get; init; }
        public string ObjectName { get; init; }
        public ApplicationAction Action { get; init; }
    }
}