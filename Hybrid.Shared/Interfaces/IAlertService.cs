namespace Hybrid.Shared.Interfaces
{
    public interface IAlertService
    {
        Task AlertAsync(string message, string title = "Alert", string buttonName = "Ok");
        Task<string> PromptAsync(string message, string title);
        Task<bool> ConfirmAsync(string message, string title = "Confirm", string buttonName = "Ok", string cancelButton = "Cancel");
    }
}
