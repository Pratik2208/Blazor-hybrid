using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Hybrid.Shared
{
    public class AppState : INotifyPropertyChanged
    {
        private const string RefreshView = "RefreshView";
        public event PropertyChangedEventHandler? PropertyChanged;
        public void NotifyPageRefreshed() => Notify(nameof(RefreshView));

        private void Notify([CallerMemberName] string name = "")
        {
            PropertyChanged!.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
