using FuelAcc.Client.Services;
using FuelAcc.Client.Shared;
using FuelAcc.Client.Shared.Api;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;
//using Microsoft.AspNetCore.SignalR.Client;

namespace FuelAcc.Client
{
    public static class ClientServicesExtensions
    {
        public static IServiceCollection ConfigureClientServices(this IServiceCollection services, Uri baseAddress)
        {
            services.AddScoped(sp => new HttpClient { BaseAddress = baseAddress });

            services.AddApiClient<ILoginApiClient, LoginApiClient>(baseAddress);
            services.AddProtectedApiClient<IProductsApiClient, ProductsApiClient>(baseAddress);

            // Register a preconfigure SignalR hub connection.
            // Note the connection isnt yet started, this will be done as part of the App.razor component
            // to avoid blocking the application startup in case the connection cannot be established

            //services.AddSingleton<HubConnection>(sp =>
            //{
            //    var navigationManager = sp.GetRequiredService<NavigationManager>();
            //    return new HubConnectionBuilder()
            //      .WithUrl(navigationManager.ToAbsoluteUri("/reporthub"))
            //      .WithAutomaticReconnect()
            //      .Build();
            //});

            // As an alternative, we could create the hubConnection here and connect to it
            // then continue with the host startup process.
            // However not this would delay rendering the app untol the signalR connection is established
            // and would prevent the app from rendering at all if an error is raised.
            // var navigationManager = host.Services.GetRequiredService<NavigationManager>();
            // var hubConnection = new HubConnectionBuilder()
            //   .WithUrl(navigationManager.ToAbsoluteUri("/surveyhub"))
            //   .WithAutomaticReconnect()
            //   .Build();
            // await hubConnection.StartAsync();

            // We could launch a task that will keep retrying indefinitely as per:
            //  https://docs.microsoft.com/en-us/aspnet/core/signalr/dotnet-client?view=aspnetcore-5.0&tabs=visual-studio#handle-lost-connection
            // We should also handle Closed connection to attempt running this code again

            // some kind of initialization?
            // maybe check on hubConnection.State == HubConnectionState.Disconnected
            // Task.Delay(20000).ContinueWith(t => Task.Run(() => hubConnection.StartAsync()));
            // Task.Run(() => hubConnection.StartAsync());

            // builder.Services.AddSingleton<HubConnection>(hubConnection);
            // await host.RunAsync();


            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAlertService, AlertService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddSingleton<IAuthenticationContext, AuthenticationContext>();
            services.AddScoped<ILocalStorageService, LocalStorageService>();
            services.AddSingleton<PageHistoryState>();


            services.AddLocalization();

            //services.AddSingleton<StateContainer>();

            return services;
        }

        private static void AddApiClient<TClient, TImplementation>(this IServiceCollection services, Uri baseAddress) where TClient : class where TImplementation : class, TClient
        {
            services.AddHttpClient<TClient, TImplementation>((serviceProvider, client) => { 
                client.BaseAddress = baseAddress; 
            });
        }
        private static void AddProtectedApiClient<TClient, TImplementation>(this IServiceCollection services, Uri baseAddress) where TClient : class where TImplementation : class, TClient
        {
            services.AddHttpClient<TClient, TImplementation>((serviceProvider, client) => {
                client.BaseAddress = baseAddress;
                var loginService = serviceProvider.GetRequiredService<IAuthenticationContext>();
                if (loginService.IsAuthenticated)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginService.Token);
                }
            });
        }


        // public static async Task<bool> ConnectWithRetryAsync(HubConnection connection, CancellationToken token)
        // {
        //     // Keep trying to until we can start or the token is canceled.
        //     while (true)
        //     {
        //         try
        //         {
        //             await connection.StartAsync(token);

        //             return true;
        //         }
        //         catch when (token.IsCancellationRequested)
        //         {
        //             return false;
        //         }
        //         catch
        //         {
        //             // Failed to connect, trying again in 5000 ms.
        //             await Task.Delay(5000);
        //         }
        //     }
        // }
    }
}