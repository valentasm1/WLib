using System;
using System.Collections.Generic;
using Wlib.Core.Mobile.Demo.ViewModels;
using Wlib.Core.Mobile.Demo.Views;
using Xamarin.Forms;

namespace Wlib.Core.Mobile.Demo
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
