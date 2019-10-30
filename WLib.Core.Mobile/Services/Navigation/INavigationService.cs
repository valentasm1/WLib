using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WLib.Core.Mobile.Services.Navigation
{
    public interface INavigationService
    {

        Task NavigateToAsync(string navigationRoute, Dictionary<string, string> args = null, NavigationOptions options = null);

        Task GoBackAsync(bool fromModal = false);
    }
}
