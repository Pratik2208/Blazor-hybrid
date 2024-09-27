using Hybrid.Shared.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Hybrid.Shared.Helper
{
    public class CustomAuthenticationStateProvider(IStorageService storageService) : AuthenticationStateProvider
    {
        private readonly IStorageService storageService = storageService;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var token = await storageService.GetAsync("Token");
                if (token == null || token == string.Empty)
                {
                    return await Task.FromResult(new AuthenticationState(_anonymous));
                }
                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, await storageService.GetAsync("UserName"))
                }));
                return new AuthenticationState(new ClaimsPrincipal(claimsPrincipal));
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
        }

        public async Task UpdateAuthenticationState()
        {
            ClaimsPrincipal claimsPrincipal;
            var token = await storageService.GetAsync("Token");

            if (token == null || token == string.Empty)
            {
                claimsPrincipal = _anonymous;
            }
            else
            {
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
               {
                    new Claim(ClaimTypes.Name, await storageService.GetAsync("UserName"))
               }, "JwtAuth"));
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
    }
}
