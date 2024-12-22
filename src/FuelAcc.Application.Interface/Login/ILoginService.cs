using FuelAcc.Application.Dto.Login;

namespace FuelAcc.Application.Interface.Login
{
    public interface ILoginService
    {
        Task<AuthResponceDto?> Login(AuthRequestDto dto);

        Task Logout();
    }
}