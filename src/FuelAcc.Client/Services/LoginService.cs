using FuelAcc.Client.Shared;
using FuelAcc.Client.Shared.Api;
using Microsoft.AspNetCore.Components;

namespace FuelAcc.Client.Services
{
    public class LoginService : ILoginService
    {
        private ILoginApiClient _apiClient;
        private ILocalStorageService _localStorageService;
        private NavigationManager _navigationManager;
        private readonly IAuthenticationContext _authenticationService;
        private string _userKey = "user";


        public LoginService(ILoginApiClient apiClient, ILocalStorageService localStorageService, NavigationManager navigationManager, IAuthenticationContext authenticationService)
        {
            _apiClient = apiClient;
            _localStorageService = localStorageService;
            _navigationManager = navigationManager;
            _authenticationService = authenticationService;
        }

        public async Task Initialize()
        {
            _authenticationService.Token = await _localStorageService.GetItem<string>(_userKey);
        }

        public async Task Login(Login model)
        {
            var res = await _apiClient.FormAsync("password", "", model.Username, model.Password, "", "");
            _authenticationService.Token = res.Access_token;
            await _localStorageService.SetItem(_userKey, res.Access_token);
        }

        public async Task Logout()
        {
            _authenticationService.Token = null;
            await _localStorageService.RemoveItem(_userKey);
            _navigationManager.NavigateTo("/user/login");
        }
    }
}