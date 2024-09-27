using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hybrid.Shared;

namespace Hybrid.Mobile.ViewModels
{
    public partial class RefreshViewModel(AppState appState) : ObservableObject
    {
        private readonly AppState appState = appState;

        [ObservableProperty]
        private bool _shouldRefresh;

        [RelayCommand]
        public void LoadNotes()
        {
            appState.NotifyPageRefreshed();
            ShouldRefresh = false;
        }
    }
}
