using System;
using System.Collections.Generic;
using System.Text;

namespace WLib.Core.Mobile.ViewModels
{
    public interface IView
    {
        IViewModel ViewModel { get; }
    }
}
