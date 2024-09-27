using Hybrid.Shared.Interfaces;
using Microsoft.JSInterop;

namespace Hybrid.Web.Services
{
    public class AlertService : IAlertService
    {
        private readonly IJSRuntime jSRuntime;
        public AlertService(IJSRuntime jSRuntime)
        {
            this.jSRuntime = jSRuntime;
        }
        public async Task AlertAsync(string message, string title = "Alert", string buttonName = "Ok")
        {
            await jSRuntime.InvokeVoidAsync("window.alert", $"{title}\n{message}");
        }

        public async Task<bool> ConfirmAsync(string message, string title = "Confirm", string buttonName = "Ok", string cancelButton = "Cancel")
        {
            return await jSRuntime.InvokeAsync<bool>("window.confirm", $"{title}\n{message}");
        }

        public async Task<string> PromptAsync(string message, string title)
        {
            return await jSRuntime.InvokeAsync<string>("window.prompt", $"{title}\n{message}");
        }
    }
}
