namespace FuelAcc.Client.Services
{
    public interface IAuthorizeApi
    {
        Task<string> Login(LoginParameters model);

        Task Logout();
    }
}