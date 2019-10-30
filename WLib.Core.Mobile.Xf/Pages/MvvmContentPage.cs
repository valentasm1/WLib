using System;
using System.Collections.Generic;
using System.Text;
using WLib.Core.Mobile.ViewModels;
using WLib.Core.Mobile.Xf.Services.AppServices;
using Xamarin.Forms;

namespace WLib.Core.Mobile.Xf.Pages
{
    public abstract class MvvmContentPage : ContentPage, IView
    {

        public MvvmContentPage()
        {
            ViewModel = CoreBootStrap.IoC.Resolve<IViewModel>(NavigationRoute);
            BindingContext = ViewModel;
        }

        public Dictionary<string, string> NavigationArgs { get; set; }

        public IViewModel ViewModel { get; private set; }
        protected abstract string NavigationRoute { get; }

        protected override async void OnAppearing()
        {
            await ViewModel.InitializeAsync(NavigationArgs);
            base.OnAppearing();
        }

    }
}
