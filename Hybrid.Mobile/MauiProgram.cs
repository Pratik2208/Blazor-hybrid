using CommunityToolkit.Maui;
using Hybrid.Mobile.Service;
using Hybrid.Mobile.ViewModels;
using Hybrid.Shared;
using Hybrid.Shared.Helper;
using Hybrid.Shared.Interfaces;
using Hybrid.Shared.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;

namespace Hybrid.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            }).UseMauiCommunityToolkit();

            // Services

            builder.Services.AddHttpClient("client", client =>
            {
                client.BaseAddress = new Uri("http://10.0.2.2:5230/");
            });

            builder.Services
                .AddSingleton<IStorageService, StorageService>()
                .AddSingleton<ILoginService, LoginService>();

            builder.Services.AddScoped<INoteService, NoteService>();

            builder.Services.AddSingleton(WebAuthenticator.Default);
            builder.Services.AddSingleton<IPlatformService, PlatformService>();
            builder.Services.AddSingleton<IAlertService, AlertService>();

            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            builder.Services.AddAuthorizationCore();

            builder.Services.AddSingleton<RefreshViewModel>();
            builder.Services.AddSingleton<AppState>();

            builder.Services.AddMauiBlazorWebView();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}