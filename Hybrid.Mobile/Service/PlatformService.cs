using Hybrid.Shared.Interfaces;

namespace Hybrid.Mobile.Service
{
    public class PlatformService : IPlatformService
    {
        private readonly Page? page;
        public PlatformService()
        {
            page = App.Current!.MainPage;
        }
        public bool IsBrowser => false;

        public async Task<string?> ChooseFromOptions(string title, params string[] options)
        {
            return await page!.DisplayActionSheet(title, "Cancel", null, options);
        }
    }
}
