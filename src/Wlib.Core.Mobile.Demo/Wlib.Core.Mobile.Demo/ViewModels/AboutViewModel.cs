using System;
using System.Threading.Tasks;
using System.Windows.Input;
using WLib.Core.Mobile.Services.AppServices;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Wlib.Core.Mobile.Demo.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await StartActivity());
        }

        private async Task StartActivity()
        {
            //DependencyService.Register<IStreamingProvider>();
            DependencyService.Get<IStreamingProvider>().ShowStreamingWindow("rtmp://62.77.152.170:1935/live/test");
        }

        public ICommand OpenWebCommand { get; }
    }
}