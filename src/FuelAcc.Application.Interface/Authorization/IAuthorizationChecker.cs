
namespace FuelAcc.Application.Interface
{
    public interface IAuthorizationChecker
    {
        public void Authorize(IAuthorizationPoint point);
        Guid UserId();
    }
}