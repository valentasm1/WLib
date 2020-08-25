using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Wlib.Core.Mobile.Demo.Services;
using Wlib.Core.Mobile.Demo.Views;

namespace Wlib.Core.Mobile.Demo
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
