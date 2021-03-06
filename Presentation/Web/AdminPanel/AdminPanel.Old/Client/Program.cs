﻿using System.Threading.Tasks;
using AdminPanel.Client.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;

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

            // Register services
            builder.Services.AddSingleton<AuthCredentialsKeeper>();
            builder.Services.AddSingleton<ApiClient>();

            // Real services
            builder.Services.AddSingleton<IAuthService, ApiAuthService>();
            builder.Services.AddSingleton<IResourceService, ApiResourceService>();

            // Mock services
            //builder.Services.AddSingleton<IAuthService, MockAuthService>();
            //builder.Services.AddSingleton<IResourceService, MockResourceService>();

            await builder.Build().RunAsync();
        }
    }
}