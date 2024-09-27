using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Hybrid.Shared.Interfaces;

namespace Hybrid.Mobile.Service
{
    public class LoginService(IStorageService storageService, IWebAuthenticator authenticator) : ILoginService
    {
        private readonly IStorageService storageService = storageService;
        private readonly IWebAuthenticator authenticator = authenticator;

        public async Task Login()
        {
            try
            {
                var result = await authenticator.AuthenticateAsync(new WebAuthenticatorOptions
                {
                    CallbackUrl = new Uri("myapp://auth"),
                    Url = new Uri("http://192.168.43.106:8080/realms/Test/protocol/openid-connect/auth?response_type=token&client_id=tripex&redirect_uri=myapp://auth")
                });
                await SetTokenAndClaims(result.AccessToken);
            }
            catch (TaskCanceledException)
            {
                string text = "Please Login";
                ToastDuration duration = ToastDuration.Short;
                double fontSize = 14;
                var toast = Toast.Make(text, duration, fontSize);
                await toast.Show(CancellationToken.None);
            }
        }

        private async Task SetTokenAndClaims(string token)
        {
            Task setToken = storageService.SaveAsync("Token", token);
            Task setClaims = storageService.SetClaims();
            await Task.WhenAll([setToken, setClaims]);
        }
    }
}
