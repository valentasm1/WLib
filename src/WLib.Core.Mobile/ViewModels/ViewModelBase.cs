using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace WLib.Core.Mobile.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class ViewModelBase : INotifyPropertyChanged, IViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual Task InitializeAsync() => Task.CompletedTask;
        public virtual Task FinalizeAsync() => Task.CompletedTask;

    }
}
