using Hybrid.Mobile.ViewModels;

namespace Hybrid.Mobile
{
    public partial class App : Application
    {
        public App(RefreshViewModel viewModel)
        {
            InitializeComponent();

            MainPage = new MainPage(viewModel);
        }
    }
}
