using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WLib.Core.Mobile.ViewModels
{
    public interface IViewModel
    {
        Task InitializeAsync();
        Task FinalizeAsync();
    }
}
