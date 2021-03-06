using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using AdminPanel.Client.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace AdminPanel.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            // Register auth
            builder.Services.AddSingleton<AuthenticationStateProvider,
                CustomAuthStateProvider>();
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore(options =>
            {
            }); // The options needs to be there for auth to work on some machines

            // Register root component
            builder.RootComponents.Add<App>("app");

            // Add local storage
            builder.Services.AddBlazoredLocalStorage();
            
            //The same as "builder.Services.AddBlazoredLocalStorage();" but as singleton scope so it works with other singleton scopes
            builder.Services.AddSingleton<ILocalStorageService, LocalStorageService>();
            builder.Services.AddSingleton<ISyncLocalStorageService, LocalStorageService>();

            // Register services
            builder.Services.AddSingleton<AuthCredentialsKeeper>();
            builder.Services.AddSingleton<ApiClient>();

            // Real services
            
            builder.Services.AddSingleton<IAuthService, ApiAuthService>();
            builder.Services.AddSingleton<IResourceService, ApiResourceService>();
            builder.Services.AddSingleton<IReservationService, ReservationService>();
            builder.Services.AddSingleton<ITicketService, ApiTicketService>();
            builder.Services.AddSingleton<IMailService, ApiMailService>();

            // Mock services
            //builder.Services.AddSingleton<IAuthService, MockAuthService>();
            //builder.Services.AddSingleton<IResourceService, MockResourceService>();

            builder.Services.AddSingleton(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


            var host = builder.Build();
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var authService = services.GetRequiredService<IAuthService>();
            //authService.Logout(); todo remove

            var syncLocalStorageService = services.GetRequiredService<ISyncLocalStorageService>();
            syncLocalStorageService.Clear();
            var localStorageService = services.GetRequiredService<ILocalStorageService>();
            await localStorageService.ClearAsync();
            await host.RunAsync();


        }
    }
}