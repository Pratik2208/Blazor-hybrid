using Hybrid.Shared.Interfaces;

namespace Hybrid.Web.Services
{
    public class PlatformService : IPlatformService
    {
        public bool IsBrowser => true;

        public Task<string?> ChooseFromOptions(string title, params string[] options)
        {
            throw new NotImplementedException();
        }
    }
}
