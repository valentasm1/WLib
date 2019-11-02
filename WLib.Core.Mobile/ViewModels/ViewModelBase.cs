using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
using WLib.Core.Mobile.Services.Navigation;

namespace WLib.Core.Mobile.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class ViewModelBase : INotifyPropertyChanged, IViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual Task InitializeAsync(Dictionary<string, string> args) => Task.CompletedTask;

    }
}
