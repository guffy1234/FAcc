using FuelAcc.Client.Services;
//using Microsoft.AspNetCore.SignalR.Client;

namespace FuelAcc.Client
{
    public class BearerTokenHandler : DelegatingHandler
    {
        public BearerTokenHandler(IAuthenticationContext tokenProvider)
        {
            TokenProvider = tokenProvider;
        }

        public IAuthenticationContext TokenProvider { get; }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string token = TokenProvider.Token;
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}