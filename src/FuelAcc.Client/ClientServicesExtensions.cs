using FuelAcc.ApiClient;
using FuelAcc.Client.Services;
using FuelAcc.Client.Services.Crud;
using FuelAcc.Client.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.ComponentModel.Design;
using System.Net.Http.Headers;
//using Microsoft.AspNetCore.SignalR.Client;

namespace FuelAcc.Client
{

    public static class ClientServicesExtensions
    {
        public static IServiceCollection ConfigureClientServices(this IServiceCollection services, Uri baseAddress)
        {
            services.AddMemoryCache();

            services.AddScoped(sp => new HttpClient { BaseAddress = baseAddress });

            services.AddApiClient<ILoginApiClient, LoginApiClient>(baseAddress);

            services.AddProtectedApiClient<IProductsApiClient, ProductsApiClient>(baseAddress);
            services.AddProtectedApiClient<IBranchesApiClient, BranchesApiClient>(baseAddress);
            services.AddProtectedApiClient<IStoragesApiClient, StoragesApiClient>(baseAddress);
            services.AddProtectedApiClient<IPartnersApiClient, PartnersApiClient>(baseAddress);

            services.AddProtectedApiClient<IOrdersInApiClient, OrdersInApiClient>(baseAddress);
            services.AddProtectedApiClient<IOrdersOutApiClient, OrdersOutApiClient>(baseAddress);
            services.AddProtectedApiClient<IOrdersMoveApiClient, OrdersMoveApiClient>(baseAddress);

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

            services.AddAuthorizationCore();
            services.AddScoped<IdentityAuthenticationStateProvider>();
            services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<IdentityAuthenticationStateProvider>());


            services.AddScoped<IAlertService, AlertService>();
            services.AddScoped<IAuthorizeApi, AuthorizeApi>();
            services.AddSingleton<IAuthenticationContext, AuthenticationContext>();
            services.AddSingleton<ILocalStorageService, LocalStorageService>();
            services.AddSingleton<PageHistoryState>();

            services.AddScoped<IDtoApiClient<ProductDto, ProductDtoPagedResult, ProductQueryDto>>(p =>
                p.GetRequiredService<IProductsApiClient>());
            services.AddScoped<IDtoApiClient<PartnerDto, PartnerDtoPagedResult, PartnerQueryDto>>(p =>
                p.GetRequiredService<IPartnersApiClient>());
            services.AddScoped<IDtoApiClient<BranchDto, BranchDtoPagedResult, BranchQueryDto>>(p =>
                p.GetRequiredService<IBranchesApiClient>());
            services.AddScoped<IDtoApiClient<StorageDto, StorageDtoPagedResult, StorageQueryDto>>(p =>
                p.GetRequiredService<IStoragesApiClient>());
            services.AddScoped<IDtoApiClient<OrderInDto, OrderInDtoPagedResult, OrderInQueryDto>>(p =>
                p.GetRequiredService<IOrdersInApiClient>());
            services.AddScoped<IDtoApiClient<OrderOutDto, OrderOutDtoPagedResult, OrderOutQueryDto>>(p =>
                p.GetRequiredService<IOrdersOutApiClient>());
            services.AddScoped<IDtoApiClient<OrderMoveDto, OrderMoveDtoPagedResult, OrderMoveQueryDto>>(p =>
                p.GetRequiredService<IOrdersMoveApiClient>());

            services.AddScoped<IDictionaryService<ProductDto>, DictionaryService<ProductDto, ProductDtoPagedResult, ProductQueryDto>>();
            services.AddScoped<IDictionaryService<PartnerDto>, DictionaryService<PartnerDto, PartnerDtoPagedResult, PartnerQueryDto>>();
            services.AddScoped<IDictionaryService<BranchDto>, DictionaryService<BranchDto, BranchDtoPagedResult, BranchQueryDto>>();
            services.AddScoped<IDictionaryService<StorageDto>, DictionaryService<StorageDto, StorageDtoPagedResult, StorageQueryDto>>();

            services.AddScoped<IDocumentService<OrderInDto>, DocumentService<OrderInDto, OrderInDtoPagedResult, OrderInQueryDto>>();
            services.AddScoped<IDocumentService<OrderOutDto>, DocumentService<OrderOutDto, OrderOutDtoPagedResult, OrderOutQueryDto>>();
            services.AddScoped<IDocumentService<OrderMoveDto>, DocumentService<OrderMoveDto, OrderMoveDtoPagedResult, OrderMoveQueryDto>>();

            services.AddLocalization();

            services.AddTransient<BearerTokenHandler>();

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
            services.AddHttpClient<TClient, TImplementation>(client => {
                client.BaseAddress = baseAddress;
            }).AddHttpMessageHandler<BearerTokenHandler>();
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