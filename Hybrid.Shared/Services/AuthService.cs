using Hybrid.Shared.Helper;
using Hybrid.Shared.Interfaces;

namespace Hybrid.Shared.Services
{
    public class AuthService(IStorageService storageService, CustomAuthenticationStateProvider provider)
    {
        private readonly IStorageService storageService = storageService;
        private readonly CustomAuthenticationStateProvider provider = provider;

        public async Task<string?> GetToken()
        {
            return await storageService.GetAsync("Token");
        }

        public void Logout()
        {
            //provider.NotifyUserLogout();
        }
    }
}
