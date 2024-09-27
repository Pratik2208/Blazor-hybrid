using Hybrid.Shared.Interfaces;

namespace Hybrid.Mobile.Service
{
    public class AlertService : IAlertService
    {
        private readonly Page? _page;
        public AlertService()
        {
            _page = App.Current!.MainPage;
        }
        public async Task AlertAsync(string message, string title = "Alert", string buttonName = "Ok")
        {
            await _page!.DisplayAlert(title, message, buttonName);
        }

        public async Task<bool> ConfirmAsync(string message, string title = "Confirm", string buttonName = "Ok", string cancelButton = "Cancel")
        {
            return await _page!.DisplayAlert(title, message, buttonName, cancelButton);
        }

        public async Task<string> PromptAsync(string message, string title)
        {
            return await _page!.DisplayPromptAsync(title, message, "Ok");
        }
    }
}
