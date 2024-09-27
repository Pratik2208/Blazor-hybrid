using Hybrid.Shared.Helper;
using Hybrid.Shared.Interfaces;

namespace Hybrid.Mobile.Service
{
    public class StorageService : IStorageService
    {
        public async Task<string?> GetAsync(string key)
        {
            return await SecureStorage.Default.GetAsync(key);
        }

        public async Task SaveAsync(string key, string value)
        {
            await SecureStorage.Default.SetAsync(key, value);
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
        public void DeleteAll()
        {
            SecureStorage.Default.RemoveAll();
        }
    }
}
