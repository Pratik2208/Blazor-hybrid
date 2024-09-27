namespace Hybrid.Shared.Interfaces
{
    public interface IPlatformService
    {
        bool IsBrowser { get; }
        Task<string?> ChooseFromOptions(string title, params string[] options);
    }
}
