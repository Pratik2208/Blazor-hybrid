using Hybrid.Mobile.ViewModels;

namespace Hybrid.Mobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage(RefreshViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
