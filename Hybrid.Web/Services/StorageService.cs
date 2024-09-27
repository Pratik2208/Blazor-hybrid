using Hybrid.Shared.Helper;
using Hybrid.Shared.Interfaces;
using Microsoft.JSInterop;

namespace Hybrid.Web.Services
{
    public class StorageService(IJSRuntime jSRuntime) : IStorageService
    {
        private readonly IJSRuntime jSRuntime = jSRuntime;
        private readonly string storageName = "window.localStorage";

        public async void DeleteAll()
        {
            await jSRuntime.InvokeVoidAsync($"{storageName}.clear");
        }

        public async Task<string?> GetAsync(string key)
        {
            return await jSRuntime.InvokeAsync<string?>($"{storageName}.getItem", key);
        }

        public async Task SaveAsync(string key, string value)
        {
            await jSRuntime.InvokeVoidAsync($"{storageName}.setItem", key, value);
        }

        public async Task SetClaims()
        {
            string? token = await GetAsync("Token");
            var securityToken = JwtTokenHelper.GetJwtToken(token!);
            await SaveAsync("Client", securityToken.Claims.FirstOrDefault(c => c.Type == "azp")!.Value);
            await SaveAsync("UserName", securityToken.Claims.FirstOrDefault(c => c.Type == "preferred_username")!.Value);
            await SaveAsync("Issuer", securityToken.Claims.FirstOrDefault(c => c.Type == "iss")!.Value);
            await SaveAsync("Exp-time", securityToken.Claims.First(c => c.Type == "exp")!.Value);
        }
    }

}
