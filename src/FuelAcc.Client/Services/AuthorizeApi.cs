using FuelAcc.Client.Shared.Api;

namespace FuelAcc.Client.Services
{
    public class AuthorizeApi : IAuthorizeApi
    {
        private ILoginApiClient _apiClient;

        public AuthorizeApi(ILoginApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<string> Login(LoginParameters model)
        {
            var res = await _apiClient.FormAsync("password", "", model.UserName, model.Password, "", "");
            return res.Access_token;
        }

        public async Task Logout()
        {
            await _apiClient.LogoutAsync();
        }
    }
}