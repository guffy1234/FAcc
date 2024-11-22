using FuelAcc.Client.Shared;

namespace FuelAcc.Client.Services
{
    public interface ILoginService
    {
        Task Initialize();
        Task Login(Login model);
        Task Logout();
    }
}