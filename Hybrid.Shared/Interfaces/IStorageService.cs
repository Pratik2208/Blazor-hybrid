namespace Hybrid.Shared.Interfaces
{
    public interface IStorageService
    {
        Task SaveAsync(string key, string value);
        Task<string?> GetAsync(string key);
        Task SetClaims();
        void DeleteAll();
    }
}
