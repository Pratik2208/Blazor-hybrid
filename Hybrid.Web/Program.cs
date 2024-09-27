using Hybrid.Shared;
using Hybrid.Shared.Interfaces;
using Hybrid.Shared.Services;
using Hybrid.Web;
using Hybrid.Web.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddOidcAuthentication(options =>
{
    options.ProviderOptions.MetadataUrl = "http://192.168.43.106:8080/realms/Test/.well-known/openid-configuration";
    options.ProviderOptions.Authority = "http://192.168.43.106:8080/realms/Test";
    options.ProviderOptions.ClientId = "tripex";
    options.ProviderOptions.ResponseType = "id_token token";

    options.UserOptions.NameClaim = "preferred_username";
    options.UserOptions.RoleClaim = "roles";
    options.UserOptions.ScopeClaim = "openid profile";
    options.ProviderOptions.ClientId = "tripex";


    options.ProviderOptions.PostLogoutRedirectUri = "http://localhost:5230/home";
});


builder.Services.AddHttpClient("client", client =>
{
    client.BaseAddress = new Uri("http://localhost:5230/");
});

builder.Services.AddSingleton<IPlatformService, PlatformService>();
builder.Services.AddScoped<INoteService, NoteService>();
builder.Services.AddSingleton<IAlertService, AlertService>();
builder.Services.AddSingleton<IStorageService, StorageService>();
builder.Services.AddSingleton<AppState>();
builder.Services.AddApiAuthorization();

await builder.Build().RunAsync();
