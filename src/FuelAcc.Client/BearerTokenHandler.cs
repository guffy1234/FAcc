using FuelAcc.Client.Services;
using Microsoft.AspNetCore.Components;
using System.Net;
//using Microsoft.AspNetCore.SignalR.Client;

namespace FuelAcc.Client
{
    public class BearerTokenHandler : DelegatingHandler
    {
        private readonly IAuthenticationContext _authContext;
        private readonly NavigationManager _navigationManager;

        public BearerTokenHandler(IAuthenticationContext tokenProvider, NavigationManager navigationManager)
        {
            _authContext = tokenProvider;
            _navigationManager = navigationManager;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string token = _authContext.Token;
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await base.SendAsync(request, cancellationToken);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _navigationManager.NavigateTo("login/logout");
            }
            return response;
        }
    }
}