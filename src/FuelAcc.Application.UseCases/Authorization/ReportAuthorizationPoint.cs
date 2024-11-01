using FuelAcc.Domain.Enums;

namespace FuelAcc.Application.UseCases.Authorization
{
    internal class ReportAuthorizationPoint : AuthorizationPoint
    {
        public ReportAuthorizationPoint() : base(ApplicationArea.Report)
        {
        }
    }
}